using HireMe.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public static class ServiceExtentions
{

  public static IServiceCollection AddJwtEnvInfrastructure(
    this IServiceCollection services,
    IConfiguration config

    )
  {
    services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IUserProfileRepo, UserProfileRepo>();
    return services;
  }
  public static void ConfigureSqlContext(
    this IServiceCollection services,
    IConfiguration config)
  {
    var connStrings = new ConnectionStrings();
    config.GetSection("ConnectionStrings").Bind(connStrings);

    var otherSettings = new OtherSetings();
    config.GetSection("OtherSettings").Bind(otherSettings);

    services.AddDbContext<AppDbContext>(opt =>
    {
      if (otherSettings.CurrentEnvironment == "dev")
      {
        opt.UseSqlServer(connStrings.DevDB);
      }
      else if (otherSettings.CurrentEnvironment == "prod")
      {
        opt.UseSqlServer(connStrings.ProdDB);
      }
      else
      {
        opt.UseSqlServer(connStrings.DevDB);
      }
    });
  }
  public static void AddAuthConfigurations(this IServiceCollection services, IConfiguration configuration)
  {
    var jwtSettings = new JwtSettings();
    configuration.GetSection("JwtSettings").Bind(jwtSettings);

    services.AddControllers().AddNewtonsoftJson(
      opt =>
      {
        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
      }
    );
    services.AddIdentity<AppUser, IdentityRole>(opt =>
    {
      var jwtSettings = new JwtSettings();
      configuration.GetSection("JwtSettings").Bind(jwtSettings);

      opt.Password.RequiredLength = 8;
    }).AddEntityFrameworkStores<AppDbContext>();

    services.AddAuthentication(opt =>
    {
      opt.DefaultAuthenticateScheme =
      opt.DefaultChallengeScheme =
      opt.DefaultForbidScheme =
      opt.DefaultScheme =
      opt.DefaultSignInScheme =
      opt.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(opt =>
    {
      opt.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings.SigningKey!))
      };
    });
  }
  public static void EnvironmentConfigurations(
    this IServiceCollection services,
    IConfiguration configuration

    )
  {
    services.Configure<JwtSettings>(configuration.GetSection("JwtSetting"));
    services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
    services.Configure<OtherSetings>(configuration.GetSection("OtherSettings"));
  }
}