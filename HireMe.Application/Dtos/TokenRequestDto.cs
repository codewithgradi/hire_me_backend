using System.ComponentModel.DataAnnotations;

public class TokenRequestDto
{
  [Required]
  public string? AccessToken { get; set; }
  [Required]
  public string? RefreshToken { get; set; }
}