using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
  private readonly IUserProfileRepo _profileRepo;
  public ProfileController(IUserProfileRepo profileRepo)
  {
    _profileRepo = profileRepo;
  }
  [HttpGet("my-profile")]
  [Authorize]
  public async Task<IActionResult> GetUser()
  {
    var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (id == null) return BadRequest("user not found");

    var user = await _profileRepo.GetByidAsync(Convert.ToInt32(id));
    if (user == null) return NotFound("User not found");
    return Ok(user);
  }
  [HttpPost]
  [Authorize]
  public async Task<IActionResult> AddUser([FromBody] AddUserProfileDto addUser)
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (string.IsNullOrEmpty(userId)) return Unauthorized();
    addUser.AppUserId = userId;
    addUser.Email = User.FindFirstValue(ClaimTypes.Email);
    var user = await _profileRepo.AddUserProfileAsync(addUser.AppUserId, addUser);
    if (user == null) return BadRequest();
    return Ok(user);
  }
  [HttpPut("my-profile")]
  [Authorize]
  public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileDto updateUser)
  {
    var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (id == null) return BadRequest("user not found");

    var user = await _profileRepo.UpdateUserProfileAsync(Convert.ToInt32(id), updateUser);
    if (user == null) return BadRequest();
    return Ok(user);
  }
}