using MySqlConnector;
using Npgsql;
using PortfolioApi.Apis;
using PortfolioApi.Controllers;
using PortfolioApi.Model;
using Refit;
using SecretsProvider;

namespace PortfolioApi;

public class Startup
{
    public void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.AddSecretsProvider("PFS");
        var tempProvider = builder.Services.BuildServiceProvider();
        ISecretsProvider secretsProvider = tempProvider.GetRequiredService<ISecretsProvider>();
        var secrets = secretsProvider.GetSecret<Secrets>();
        
        builder.Services.AddTransient<GithubAuthHandler>();
        builder.Services.AddRefitClient<IGithubApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.github.com"))
            .AddHttpMessageHandler<GithubAuthHandler>();
        
        builder.Services.AddTransient<DroneAuthHandler>();
        builder.Services.AddRefitClient<IDroneApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://drone.tobias-huebner.tech"))
            .AddHttpMessageHandler<DroneAuthHandler>();

        builder.Services.AddRefitClient<IGhostApi>()
            .ConfigureHttpClient(c =>
            {
                if(secrets.GhostUrl != null) c.BaseAddress = new Uri(secrets.GhostUrl);
            });
        
        builder.Services.AddRefitClient<ISuggestAdminApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://suggest-app.com"));
        
        builder.Services.AddSingleton<SingletonStatsProvider>();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
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