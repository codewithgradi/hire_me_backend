public static class UserProfileMapper
{
  public static UserProfileDto ToUserProfileDto(this UserProfile profileDto)
  {
    return new UserProfileDto
    {
      Id = profileDto.Id,
      FirstName = profileDto.FirstName,
      Surname = profileDto.Surname,
      Email = profileDto.Email,
      GitHubUrl = profileDto.GitHubUrl,
      Institution = profileDto.Institution,
      LinkedInUrl = profileDto.LinkedInUrl,
      PersonalWebsite = profileDto.PersonalWebsite,
      PhoneNumber = profileDto.PhoneNumber,
      Qualification = profileDto.Qualification
    };
  }
  public static UserProfile ToUserProfile(this AddUserProfileDto profileDto)
  {
    return new UserProfile
    {
      Surname = profileDto.Surname,
      Email = profileDto.Email!,
      GitHubUrl = profileDto.GitHubUrl,
      Institution = profileDto.Institution,
      LinkedInUrl = profileDto.LinkedInUrl,
      PersonalWebsite = profileDto.PersonalWebsite,
      PhoneNumber = profileDto.PhoneNumber,
      Qualification = profileDto.Qualification
    };
  }
}