using Refit;

namespace PortfolioApi;

public interface IGhostApi
{
    [Get("/ghost/api/v3/content/posts/?key={ghostKey}&limit=1")]
    public Task<GhostPostsResponse> GetPosts(string ghostKey);
}