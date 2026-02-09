using HireMe.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class UserProfileRepo : IUserProfileRepo
{
  private readonly AppDbContext _context;
  public UserProfileRepo(AppDbContext context)
  {
    _context = context;
  }

  public async Task<UserProfileDto> AddUserProfileAsync(string appId, AddUserProfileDto userProfileDto)
  {
    if (await ProfileExists(appId)) return null;
    var model = userProfileDto.ToUserProfileFromAdd();
    model.AppUserId = appId;
    await _context.UserProfiles.AddAsync(model);
    await _context.SaveChangesAsync();

    return model.ToUserProfileDto();
  }


  public async Task<UserProfileDto?> GetByidAsync(int id)
  {
    var UserProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.Id == id);
    if (UserProfile == null) return null;
    return UserProfile.ToUserProfileDto();
  }

  public async Task<UserProfileDto> UpdateUserProfileAsync(int id, UpdateUserProfileDto updatedProfile)
  {
    var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.Id == id);
    if (userProfile == null) return null!;

    // Basic validation: Don't allow overwriting with NULL if the DB requires it
    if (!string.IsNullOrEmpty(updatedProfile.Email))
    {
      userProfile.Email = updatedProfile.Email;
    }

    userProfile.GitHubUrl = updatedProfile.GitHubUrl;
    userProfile.FirstName = updatedProfile.FirstName;
    userProfile.Surname = updatedProfile.Surname;
    userProfile.PhoneNumber = updatedProfile.PhoneNumber;
    userProfile.Qualification = updatedProfile.Qualification;
    userProfile.LinkedInUrl = updatedProfile.LinkedInUrl;
    userProfile.PersonalWebsite = updatedProfile.PersonalWebsite;
    userProfile.Institution = updatedProfile.Institution;

    await _context.SaveChangesAsync();
    return userProfile.ToUserProfileDto();
  }
  public async Task<bool> ProfileExists(string appUserId)
  {
    return await _context.UserProfiles.AnyAsync(x => x.AppUserId == appUserId);
  }
}

