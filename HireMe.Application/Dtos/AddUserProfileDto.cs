public class AddUserProfileDto
{
  public string Name { get; set; } = string.Empty;
  public string Surname { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public QualificationType Qualification { get; set; }
  public string? Email { get; set; }
  public string LinkedInUrl { get; set; } = string.Empty;
  public string? Institution { get; set; }
  public string GitHubUrl { get; set; } = string.Empty;
  public string PersonalWebsite { get; set; } = string.Empty;
}