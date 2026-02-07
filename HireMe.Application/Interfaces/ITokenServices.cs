public interface ITokenService
{
  string CreateToken(AppUser appUSer);
  string GenerateRefreshToken();
}