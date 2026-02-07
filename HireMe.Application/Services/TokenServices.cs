using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HireMe.Application;

public class TokenService : ITokenService
{
  private readonly IConfiguration _config;
  private readonly SymmetricSecurityKey _key;
  public TokenService(IConfiguration configuration)
  {
    _config = configuration;
    _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]!));
  }

  public string CreateToken(AppUser user)
  {
    var claims = new List<Claim>
    {
      new Claim(JwtRegisteredClaimNames.NameId,user.Id),
      new Claim(JwtRegisteredClaimNames.Email,user.Email!)
    };
    var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.Now.AddMinutes(60),
      SigningCredentials = creds,
      Issuer = _config["JWT:Issuer"],
      Audience = _config["JWT:Audience"]
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
}
