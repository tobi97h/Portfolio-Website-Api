using MySqlConnector;
using PortfolioApi.Controllers;
using Refit;
using SecretsProvider;

namespace PortfolioApi;

public class Startup
{
    public void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.AddSecretsProvider("PF");
        var tempProvider = builder.Services.BuildServiceProvider();
        ISecretsProvider secretsProvider = tempProvider.GetRequiredService<ISecretsProvider>();
        var secrets = secretsProvider.GetSecret<Secrets>();
        
        builder.Services.AddTransient<GithubAuthHandler>();
        builder.Services.AddRefitClient<IGithubApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.github.com"))
            .AddHttpMessageHandler<GithubAuthHandler>();
        
        
        builder.Services.AddRefitClient<IGhostApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(secrets.GhostUrl));
        
        builder.Services.AddSingleton<SingletonStatsProvider>();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        builder.Services.AddTransient<MySqlConnection>(provider => new MySqlConnection(provider.GetRequiredService<ISecretsProvider>().GetSecret<Secrets>()
            .DBConnectionString));
    }

    public async Task Configure(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        using (var scope = app.Services.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<SingletonStatsProvider>().GetStats();
        }
        
        app.MapControllers();
        
        app.Run();
        
    }
}