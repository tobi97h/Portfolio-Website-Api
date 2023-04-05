using PortfolioApi.Model;
using Refit;

namespace PortfolioApi.Apis;

[Headers("User-Agent: StatsAgent")]
public interface IGithubApi
{
    [Get("/user/repos")]
    public Task<Repo[]> GetUserRepos();

    [Get("/repos/{owner}/{repo}/zipball")]
    public Task<HttpResponseMessage> DownloadRepo(string owner, string repo);

    [Get("/repos/{owner}/{repo}/commits?per_page=1")]
    public Task<ApiResponse<Commit[]>> GetCommits(string owner, string repo);
}