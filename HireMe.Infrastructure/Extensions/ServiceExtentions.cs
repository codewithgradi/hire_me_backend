using HireMe.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

public static class ServiceExtentions
{
  public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<AppDbContext>(opt =>
    {
      if (Env.OTHER._Environment == "dev")
      {
        opt.UseSqlServer(Env.ConnectionStrings.DevDB);
      }
      else if (Env.OTHER._Environment == "prod")
      {
        opt.UseSqlServer(Env.ConnectionStrings.ProdDB);
      }
    });
  }
  public static void AddAuthConfigurations(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped<ITokenService, TokenService>();

    services.AddControllers().AddNewtonsoftJson(
      opt =>
      {
        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
      }
    );
    services.AddIdentity<AppUser, IdentityRole>(opt =>
    {
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
        ValidIssuer = Env.JWT.Issuer,
        ValidateAudience = true,
        ValidAudience = Env.JWT.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Env.JWT.SigningKey))
      };
    });
  }
  public static void AddScopedConfigurations(this IServiceCollection services)
  {
    services.AddScoped<IUserProfileRepo, UserProfileRepo>();
  }
}