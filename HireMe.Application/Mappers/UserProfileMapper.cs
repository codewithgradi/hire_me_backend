
public static class UserProfileMapper
{
  private static string gitHubMainUrl { get; set; } = "https://www.github.com";
  private static string linkedInMainUrl { get; set; } = "https://www.github.com";
  public static UserProfileDto ToUserProfileDto(this UserProfile profile)
  {
    return new UserProfileDto
    {
      Id = profile.Id,
      FirstName = profile.FirstName,
      Surname = profile.Surname,
      Email = profile.Email,
      Institution = profile.Institution,

      GitHubUrl = profile.GitHubUrl.StartsWith("http")
                    ? profile.GitHubUrl
                    : $"{gitHubMainUrl}/{profile.GitHubUrl}",

      LinkedInUrl = profile.LinkedInUrl.StartsWith("http")
                      ? profile.LinkedInUrl
                      : $"{linkedInMainUrl}/{profile.LinkedInUrl}",

      PersonalWebsite = profile.PersonalWebsite,
      PhoneNumber = profile.PhoneNumber,
      Qualification = profile.Qualification,
      AppUserId = profile.AppUserId
    };
  }
  public static UserProfileDto ToUserProfileDtoFromAdd(this AddUserProfileDto profileDto)
  {

    return new UserProfileDto
    {
      FirstName = profileDto.FirstName,
      Surname = profileDto.Surname,
      Email = profileDto.Email,
      GitHubUrl = $"{gitHubMainUrl}/{profileDto.GitHubUrl}",
      LinkedInUrl = $"{linkedInMainUrl}/{profileDto.GitHubUrl}",
      Institution = profileDto.Institution,
      PersonalWebsite = profileDto.PersonalWebsite,
      PhoneNumber = profileDto.PhoneNumber,
      Qualification = profileDto.Qualification
    };
  }
  public static UserProfile ToUserProfileFromAdd(this AddUserProfileDto profileDto)
  {
    return new UserProfile
    {
      FirstName = profileDto.FirstName,
      Surname = profileDto.Surname,
      Email = profileDto.Email,
      GitHubUrl = $"{gitHubMainUrl}/{profileDto.GitHubUrl}",
      LinkedInUrl = $"{linkedInMainUrl}/{profileDto.GitHubUrl}",
      Institution = profileDto.Institution,
      PersonalWebsite = profileDto.PersonalWebsite,
      PhoneNumber = profileDto.PhoneNumber,
      Qualification = profileDto.Qualification
    };
  }
}