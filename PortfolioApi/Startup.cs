using MySqlConnector;
using Refit;
using SecretsProvider;

namespace PortfolioApi;

public class Startup
{
    public void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.AddSecretsProvider("PF");
        builder.Services.AddTransient<GithubAuthHandler>();
        builder.Services.AddRefitClient<IGithubApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.github.com"))
            .AddHttpMessageHandler<GithubAuthHandler>();
        
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
  
        app.MapControllers();
        
        app.Run();
        
    }
}