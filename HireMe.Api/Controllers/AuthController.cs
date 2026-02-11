using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
  private readonly SignInManager<AppUser> _signInManager;
  private readonly UserManager<AppUser> _userManager;
  private readonly ITokenService _tokenService;
  private readonly OtherSetings _otherSettings;
  public AuthController(
    IOptions<OtherSetings> otherSettings,
    UserManager<AppUser> userManager,
    ITokenService tokenService,
    SignInManager<AppUser> signInManager)
  {
    _signInManager = signInManager;
    _otherSettings = otherSettings.Value;
    _userManager = userManager;
    _tokenService = tokenService;
  }
  [HttpPost("login")]
  public async Task<IActionResult> LoginAsync(LoginDto loginDto)
  {
    try
    {
      var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
      if (user == null) return Unauthorized("Invalid Credentials");
      var loginResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password!, false);
      if (!loginResult.Succeeded) return Unauthorized("Username or Password is incorrect");
      var AccessToken = _tokenService.CreateToken(user);
      var RefreshToken = _tokenService.GenerateRefreshToken();
      user.RefreshToken = RefreshToken;
      user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
      await _userManager.UpdateAsync(user);
      return Ok(new NewUserDto
      {
        Email = loginDto.Email,
        AccessToken = AccessToken,
        RefreshToken = RefreshToken,
      });
    }
    catch (Exception e)
    {
      return StatusCode(500, e);
    }
  }

  [HttpPost("logout")]
  [Authorize]
  public async Task<IActionResult> LogoutAsync()
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (string.IsNullOrEmpty(userId)) return Unauthorized();
    var user = await _userManager.FindByIdAsync(userId);
    if (user == null) return NotFound("User not found in Database.");
    user.RefreshToken = null;
    user.RefreshTokenExpiryTime = null;
    await _userManager.UpdateAsync(user);
    return Ok("Logged out successfully.");
  }

  [HttpPost("register")]
  public async Task<IActionResult> RegisterAsync(CreateUser userRegister)
  {
    var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == userRegister.Email);
    if (user != null) return Unauthorized("User exists");
    try
    {
      var appUser = new AppUser
      {
        Email = userRegister.Email,
        UserName = userRegister.Email
      };

      var createdUser = await _userManager.CreateAsync(appUser, userRegister.Password!);
      if (!createdUser.Succeeded) return StatusCode(500, createdUser.Errors);

      var roleResult = await _userManager.AddToRoleAsync(appUser, "USER");
      if (!roleResult.Succeeded) return StatusCode(500, roleResult.Errors);

      var AccessToken = _tokenService.CreateToken(appUser);
      var RefreshToken = _tokenService.GenerateRefreshToken();

      appUser.RefreshToken = RefreshToken;
      appUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
      await _userManager.UpdateAsync(appUser);

      return Ok(new NewUserDto
      {
        Email = appUser.Email,
        AccessToken = AccessToken,
        RefreshToken = RefreshToken,
      });
    }
    catch (Exception e)
    {
      return StatusCode(500, e);
    }
  }
  [HttpPost("refresh")]
  public async Task<IActionResult> Refresh([FromBody] TokenRequestDto tokenRequestDto)
  {
    if (tokenRequestDto == null) return BadRequest("Invalid client request");

    var principal = _tokenService.GetClaimsPrincipalFromExpiredToken(tokenRequestDto.AccessToken);

    var email = principal.FindFirstValue(ClaimTypes.Email)
    ?? principal.FindFirstValue(JwtRegisteredClaimNames.Email);

    if (string.IsNullOrEmpty(email)) return BadRequest("Invalid token claims");

    var user = await _userManager.FindByEmailAsync(email);

    if (user == null ||
        user.RefreshToken != tokenRequestDto.RefreshToken ||
        user.RefreshTokenExpiryTime <= DateTime.UtcNow)
    {
      return BadRequest("Invalid refresh token or session expired");
    }

    var newAccessToken = _tokenService.CreateToken(user);
    var newRefreshToken = _tokenService.GenerateRefreshToken();

    user.RefreshToken = newRefreshToken;
    user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
    await _userManager.UpdateAsync(user);

    return Ok(new TokenRequestDto
    {
      AccessToken = newAccessToken,
      RefreshToken = newRefreshToken
    });
  }
  [HttpGet("login-google")]
  public IActionResult LoginGoogle()
  {
    var redirectUrl = Url.Action("GoogleResponse", "Auth");
    var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
    return Challenge(properties, "Google");
  }
  [HttpGet("google-response")]
  public async Task<IActionResult> GoogleResponse()
  {

    var info = await _signInManager.GetExternalLoginInfoAsync();
    if (info == null) return BadRequest("External login info missing.");

    var email = info.Principal.FindFirstValue(ClaimTypes.Email);
    var user = await _userManager.FindByEmailAsync(email);

    if (user == null)
    {
      user = new AppUser
      {
        UserName = email,
        Email = email,
        EmailConfirmed = true
      };
      var createResult = await _userManager.CreateAsync(user);
      if (!createResult.Succeeded) return BadRequest(createResult.Errors);
    }
    else
    {
      var existingLogins = await _userManager.GetLoginsAsync(user);
      var isLinked = existingLogins.Any(x => x.LoginProvider == "Google");

      if (!isLinked)
      {
        var linkResult = await _userManager.AddLoginAsync(user, info);
        if (!linkResult.Succeeded) return BadRequest("Failed to link Google account.");
      }
    }

    var accessToken = _tokenService.CreateToken(user);
    var refreshToken = _tokenService.GenerateRefreshToken();
    user.RefreshToken = refreshToken;
    user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
    await _userManager.UpdateAsync(user);

    return Redirect($"{_otherSettings.FrontEndUrl}/auth-success?token={accessToken}&refreshToken={refreshToken}");
  }

}