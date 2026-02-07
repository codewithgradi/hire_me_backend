using System.IdentityModel.Tokens.Jwt;
using HireMe.Application;
using HireMe.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MyApp.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddScoped<IUserProfileRepo, UserProfileRepo>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString
        ("DevDB"));
});
builder.Services.AuthConfigurations(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.Run();

