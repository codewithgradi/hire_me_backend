public interface IIdentityService
{
  Task LoginAsync(LoginDto loginDto);
  Task RegisterAsync(CreateUser user);
  Task LogoutAsync();
}