using Microsoft.Extensions.Configuration;

public static class Env
{
  public static IConfiguration? Config { get; set; }
  public static class ConnectionStrings
  {
    public static string DevDB => Config["Settings__DevDB"] ?? "";
    public static string ProdDB => Config["Settings__ProdDB"] ?? "";
  }

  public static class JWT
  {
    public static string Issuer => Config["Settings__Issuer"] ?? "";
    public static string Audience => Config["Settings__Audience"] ?? "";
    public static string SigningKey => Config["Settings__SigningKey"] ?? "";
  }

  public static class OTHER
  {
    public static string _Environment => Config["Settings__Environment"] ?? "";
  }
}