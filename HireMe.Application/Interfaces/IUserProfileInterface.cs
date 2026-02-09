public interface IUserProfileRepo
{
  Task<UserProfileDto?> GetByidAsync(int id);
  Task<bool> ProfileExists(string appUserId);
  Task<UserProfileDto> AddUserProfileAsync(string appId, AddUserProfileDto userProfileDto);
  Task<UserProfileDto> UpdateUserProfileAsync(int id, UpdateUserProfileDto updatedProfile);
}