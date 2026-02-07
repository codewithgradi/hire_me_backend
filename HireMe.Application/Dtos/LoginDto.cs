using System.ComponentModel.DataAnnotations;

public class LoginDto
{
  [EmailAddress]
  public string? Email { get; set; }
  [MinLength(8, ErrorMessage = "Password must be at least 8 character long")]
  public string? Password { get; set; }
}