using System.Security.Claims;

public interface ITokenService
{
  string CreateToken(AppUser appUSer);
  string GenerateRefreshToken();
  ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token);
}