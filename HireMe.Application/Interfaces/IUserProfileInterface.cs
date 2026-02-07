public interface IUserProfileRepo
{
  Task<UserProfile?> GetByEmailAsync(string email);
  Task<UserProfile> AddUserProfileAsync(AddUserProfileDto userProfileDto);
  Task<UserProfile> UpdateUserProfileAsync(Guid id, UpdateUserProfileDto updatedProfile);
}