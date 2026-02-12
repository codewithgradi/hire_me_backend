public class UserProfile
{
  public int Id { get; set; }
  public string? AppUserId { get; set; }
  public string FirstName { get; set; } = string.Empty;
  public string Surname { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public QualificationType Qualification { get; set; }
  public string LinkedInUrl { get; set; } = string.Empty;
  public string? Institution { get; set; }
  public string GitHubUrl { get; set; } = string.Empty;
  public string PersonalWebsite { get; set; } = string.Empty;
  public string? QualificationName { get; set; }
  public AppUser? AppUser { get; set; }
}