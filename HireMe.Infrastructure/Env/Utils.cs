public class JwtSettings
{
  public string? Issuer { get; set; }
  public string? Audience { get; set; }
  public string? SigningKey { get; set; }
}
public class GoogleSettings
{
  public string? ClientId { get; set; }
  public string? ClientSecret { get; set; }
}
public class ConnectionStrings
{
  public string? DevDB { get; set; }
  public string? ProdDB { get; set; }
}

public class OtherSetings
{
  public string? CurrentEnvironment { get; set; }
  public string? FrontEndUrl { get; set; }
}