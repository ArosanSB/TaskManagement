using Infrastructure.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.IntegrationTest.Utility;

public class TestEnvironmnet
{
    private readonly TestServer _testServer;

    public TestServer TestServer => _testServer;

    public HttpClient HttpClient => _testServer.CreateClient();

    public IServiceScope CreateScope() => _testServer.Services.CreateScope();

    public TestEnvironmnet()
    {
        string applicationPath = ".";
        _testServer = new TestServer(new WebHostBuilder()
            .UseEnvironment("Testing")
            .UseContentRoot(applicationPath)
            .UseConfiguration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build())
            .UseStartup<TestEnvironmentStartup>());
    }


    public void CleanDB()
    {
        // Clean the database
        BusinessLogicDbContext context = _testServer.Services.GetRequiredService<BusinessLogicDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
}
