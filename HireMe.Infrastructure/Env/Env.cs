public static class Env
{
  public static class ConnectionStrings
  {
    public static string DevDB => Environment.GetEnvironmentVariable("Settings__DevDB") ?? "";
    public static string ProdDB => Environment.GetEnvironmentVariable("Settings__ProdDB") ?? "";
  }

  public static class JWT
  {
    public static string Issuer => Environment.GetEnvironmentVariable("Settings__Issuer") ?? "";
    public static string Audience => Environment.GetEnvironmentVariable("Settings__Audience") ?? "";
    public static string SigningKey => Environment.GetEnvironmentVariable("Settings__SigningKey") ?? "";
  }

  public static class OTHER
  {
    public static string _Environment => Environment.GetEnvironmentVariable("Settings__Environment") ?? "";
  }
}