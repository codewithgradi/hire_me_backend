using HireMe.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class UserProfileRepo : IUserProfileRepo
{
  private readonly AppDbContext _context;
  public UserProfileRepo(AppDbContext context)
  {
    _context = context;
  }

  public async Task<UserProfile> AddUserProfileAsync(AddUserProfileDto userProfileDto)
  {
    await _context.UserProfiles.AddAsync(userProfileDto.ToUserProfile());
    await _context.SaveChangesAsync();
    return userProfileDto.ToUserProfile();
  }


  public async Task<UserProfile?> GetByEmailAsync(string email)
  {
    var UserProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.Email == email);
    if (UserProfile == null) return null;
    return UserProfile;
  }

  public async Task<UserProfile> UpdateUserProfileAsync(Guid id, UpdateUserProfileDto updatedProfile)
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
    return UserProfile;
  }
}

