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
    var UserProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.Id == id);
    if (UserProfile == null) return null!;
    UserProfile.GitHubUrl = updatedProfile.GitHubUrl;
    UserProfile.FirstName = updatedProfile.FirstName;
    UserProfile.Surname = updatedProfile.Surname;
    UserProfile.Email = updatedProfile.Email!;
    UserProfile.PhoneNumber = updatedProfile.PhoneNumber;
    UserProfile.Qualification = updatedProfile.Qualification;
    UserProfile.LinkedInUrl = updatedProfile.LinkedInUrl;
    UserProfile.PersonalWebsite = updatedProfile.PersonalWebsite;
    UserProfile.Institution = updatedProfile.Institution;
    _context.UserProfiles.Update(UserProfile);
    await _context.SaveChangesAsync();
    return UserProfile.ToUserProfileDto();
  }
  public async Task<bool> ProfileExists(string appUserId)
  {
    return await _context.UserProfiles.AnyAsync(x => x.AppUserId == appUserId);
  }
}

