using System.Net.Http.Headers;
using System.Text;
using PortfolioApi.Model;
using SecretsProvider;

namespace PortfolioApi.Apis;

public class GithubAuthHandler : DelegatingHandler
{
    private readonly ISecretsProvider _secretsProvider;

    public GithubAuthHandler(ISecretsProvider secretsProvider)
    {
        _secretsProvider = secretsProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = _secretsProvider.GetSecret<Secrets>();
        var base64Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(token.GithubUser + ":" + token.GithubToken));
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64Value);
        return await base.SendAsync(request, cancellationToken);
    }
}