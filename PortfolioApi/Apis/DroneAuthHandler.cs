using System.Net.Http.Headers;
using System.Text;
using PortfolioApi.Model;
using SecretsProvider;

namespace PortfolioApi.Apis;

public class DroneAuthHandler : DelegatingHandler
{
    private readonly ISecretsProvider _secretsProvider;

    public  DroneAuthHandler(ISecretsProvider secretsProvider)
    {
        _secretsProvider = secretsProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = _secretsProvider.GetSecret<Secrets>();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.DroneToken);
        return await base.SendAsync(request, cancellationToken);
    }
}