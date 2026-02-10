using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class TokenService : ITokenService
{
  private readonly IConfiguration _config;
  private readonly SymmetricSecurityKey _key;
  public TokenService(IConfiguration config)
  {
    _config = config;
    _key = new SymmetricSecurityKey
    (Encoding.UTF8.GetBytes
    (_config["Settings:SigningKey"]!));
  }
  public string CreateToken(AppUser user)
  {
    var claims = new List<Claim>
    {
      new Claim(JwtRegisteredClaimNames.NameId,user.Id),
      new Claim(JwtRegisteredClaimNames.Email,user.Email!),
      new Claim(JwtRegisteredClaimNames.Name,user.Email!),
    };
    var creds = new SigningCredentials
    (_key, SecurityAlgorithms.HmacSha512Signature);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.Now.AddMinutes(5),
      SigningCredentials = creds,
      Issuer = Env.JWT.Issuer,
      Audience = Env.JWT.Audience

    };
    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }
  public string GenerateRefreshToken()
  {
    var randomNumber = new byte[64];
    using var rng = RandomNumberGenerator.Create();
    rng.GetBytes(randomNumber);
    return Convert.ToBase64String(randomNumber);
  }
  public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token)
  {
    if (token.StartsWith("Bearer "))
    {
      token = token.Substring(7);
    }
    var tokenValidationParameters = new TokenValidationParameters
    {
      ValidateAudience = false,
      ValidateIssuer = false,
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = _key,
      ValidateLifetime = false,
    };
    var tokenHandler = new JwtSecurityTokenHandler();
    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
    if (securityToken is not JwtSecurityToken jwtSecurityToken ||
      !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase)
    ) throw new SecurityTokenException("Invalid token");
    return principal;
  }

}