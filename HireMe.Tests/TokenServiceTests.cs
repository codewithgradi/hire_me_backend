using FluentAssertions;
using Microsoft.Extensions.Options;

namespace HireMe.Tests;

public class TokenServiceTests
{
    private readonly TokenService _tokenService;
    private readonly JwtSettings _jwtSettings;
    public TokenServiceTests()
    {

        _jwtSettings = new JwtSettings
        {
            SigningKey = "this_is_a_very_long_secret_key_that_is_at_least_64_characters_long_for_hs512",
            Issuer = "TestIssuer",
            Audience = "TestAudience"
        };

        var options = Options.Create(_jwtSettings);

        _tokenService = new TokenService(options);
    }

    [Fact]
    public void CreateToken_ShouldReturnValidJwtString()
    {
        var user = new AppUser { Id = "1", Email = "test@hireme.com", UserName = "testuser" };
        var result = _tokenService.CreateToken(user);
        result.Should().NotBeNullOrEmpty();
        result.Split('.').Should().HaveCount(3);
    }
}