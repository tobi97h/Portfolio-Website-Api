using PortfolioApi.Model;
using Refit;

namespace PortfolioApi.Apis;

public interface IDroneApi
{
    [Get("/api/user/repos")]
    public Task<PipelineRepo[]> GetRepos();
}