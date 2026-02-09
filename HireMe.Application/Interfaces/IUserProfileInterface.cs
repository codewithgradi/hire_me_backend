public interface IUserProfileRepo
{
  Task<UserProfileDto?> GetByEmailAsync(string email);
  Task<UserProfileDto> AddUserProfileAsync(AddUserProfileDto userProfileDto);
  Task<UserProfileDto> UpdateUserProfileAsync(int id, UpdateUserProfileDto updatedProfile);
}