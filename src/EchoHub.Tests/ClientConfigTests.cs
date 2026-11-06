using EchoHub.Client.Config;
using Xunit;

namespace EchoHub.Tests;

public class ClientConfigTests
{
    [Theory]
    [InlineData("https://echo.example.com/", "https://echo.example.com")]
    [InlineData("https://echo.example.com", "https://echo.example.com/")]
    [InlineData("https://echo.example.com", "https://echo.example.com")]
    [InlineData("https://ECHO.example.com/", "https://echo.example.com")]
    public void FindServer_IgnoresTrailingSlashAndCase(string savedUrl, string lookupUrl)
    {
        var config = new ClientConfig();
        var server = new SavedServer { Name = "echo", Url = savedUrl };
        config.SavedServers.Add(server);

        Assert.Same(server, config.FindServer(lookupUrl));
    }

    [Fact]
    public void FindServer_OtherServer_ReturnsNull()
    {
        var config = new ClientConfig();
        config.SavedServers.Add(new SavedServer { Name = "echo", Url = "https://echo.example.com/" });

        Assert.Null(config.FindServer("https://other.example.com"));
    }
}
