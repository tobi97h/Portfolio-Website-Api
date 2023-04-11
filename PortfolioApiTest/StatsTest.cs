using Microsoft.AspNetCore.Mvc.Testing;
using PortfolioApi;

namespace PortfolioApiTest;

public class StatsTest : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly WebApplicationFactory<Startup> _factory;
    
    public StatsTest(WebApplicationFactory<Startup> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task TestStats()
    {
        var client = _factory.CreateClient();

        var statsResponse = await client.GetAsync("/stats/stats");
        var statsBody = await statsResponse.Content.ReadAsStringAsync();
        Assert.NotEmpty(statsBody);
    }
    
    [Fact]
    public async Task Minutes()
    {
        var client = _factory.CreateClient();

        var stats= await client.GetAsync("/stats/suggest/minutes");
        var respStr = await stats.Content.ReadAsStringAsync();
    }
    
    [Fact]
    public async Task Users()
    {
        var client = _factory.CreateClient();

        var stats= await client.GetAsync("/stats/suggest/users");
        var respStr = await stats.Content.ReadAsStringAsync();
    }
}