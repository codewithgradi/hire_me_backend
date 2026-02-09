using System.ComponentModel.DataAnnotations;

public class UpdateUserProfileDto
{
  [MinLength(3, ErrorMessage = "First name should have at least 3 characters.")]
  public string FirstName { get; set; } = string.Empty;
  [MinLength(3, ErrorMessage = "Surname should have at least 3 characters.")]
  public string Surname { get; set; } = string.Empty;
  [MaxLength(10, ErrorMessage = "Phone number must be ten digits long")]
  [MinLength(10, ErrorMessage = "Phone number must be ten digits long")]
  public string PhoneNumber { get; set; } = string.Empty;
  public QualificationType Qualification { get; set; }
  public string? Email { get; set; }
  public string LinkedInUrl { get; set; } = string.Empty;
  public string? Institution { get; set; }
  public string GitHubUrl { get; set; } = string.Empty;
  public string PersonalWebsite { get; set; } = string.Empty;
}
