public static class Env
{
  public static class ConnectionStrings
  {
    public static string DevDB => GetRequired("Settings__DevDB");
    public static string ProdDB => GetRequired("Settings__ProdDB");
  }

  public static class JWT
  {
    public static string Issuer => GetRequired("Settings__Issuer");
    public static string Audience => GetRequired("Settings__Audience");
    public static string SigningKey => GetRequired("Settings__SigningKey");
  }

  public static class OTHER
  {
    public static string _Environment => Environment.GetEnvironmentVariable("Settings__Environment") ?? "dev";
  }

  private static string GetRequired(string key)
  {
    var value = Environment.GetEnvironmentVariable(key);
    if (string.IsNullOrWhiteSpace(value))
    {
      throw new InvalidOperationException($"Critical Configuration Missing: The environment variable '{key}' is not set. Check your .env file.");
    }
    return value;
  }
}