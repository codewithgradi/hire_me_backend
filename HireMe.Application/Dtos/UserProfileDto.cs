public class UserProfileDto
{
  public int Id { get; set; }
  public string FirstName { get; set; } = string.Empty;
  public string Surname { get; set; } = string.Empty;
  public string? AppUserId { get; set; }

  public string PhoneNumber { get; set; } = string.Empty;
  public QualificationType Qualification { get; set; }
  public string? Email { get; set; }
  public string LinkedInUrl { get; set; } = string.Empty;
  public string? Institution { get; set; }
  public string GitHubUrl { get; set; } = string.Empty;
  public AppUser? AppUser { get; set; }
  public string PersonalWebsite { get; set; } = string.Empty;
}