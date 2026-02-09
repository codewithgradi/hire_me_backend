using System.IdentityModel.Tokens.Jwt;


var builder = WebApplication.CreateBuilder(args);

Env.Config = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddOpenApi();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

//Dependency Injections from Infrastructure Layer

builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.AddAuthConfigurations(builder.Configuration);

builder.Services.AddScopedConfigurations();

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

