using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace HireMe.Tests;

public class TokenServiceTests
{
    private readonly TokenService _tokenService;
    private readonly Mock<IConfiguration> _configMock;
    public TokenServiceTests()
    {
        _configMock = new Mock<IConfiguration>();
        _configMock.Setup(x => x["Settings:SigningKey"])
        .Returns("this_is_a_very_long_secret_key_that_is_at_least_64_characters_long_for_hs512");
        _tokenService = new TokenService(_configMock.Object);
    }
    [Fact]
    public void CreateToken_ShouldReturnValidJwtString()
    {
        // Given
        var user = new AppUser { Id = "1", Email = "test@hireme.com" };
        // When
        var result = _tokenService.CreateToken(user);
        // Then
        result.Should().NotBeNullOrEmpty();

        result.Split(',').Should().HaveCount(3);
    }
}
