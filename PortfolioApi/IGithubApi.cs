using Refit;

namespace PortfolioApi;

[Headers("User-Agent: StatsAgent")]
public interface IGithubApi
{
    [Get("/user/repos")]
    public Task<Repo[]> GetUserRepos();

    [Get("/repos/{owner}/{repo}/commits?per_page=1")]
    public Task<ApiResponse<Commit[]>> GetCommits(string owner, string repo);
}