using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;
using dotenv.net;

DotEnv.Load();


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters
        .Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddOpenApi();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

//Dependency Injections from Infrastructure Layer
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.EnvironmentConfigurations(builder.Configuration);
builder.Services.AddJwtEnvInfrastructure(builder.Configuration);
builder.Services.AddAuthConfigurations(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

