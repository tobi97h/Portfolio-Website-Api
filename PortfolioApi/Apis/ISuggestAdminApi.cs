using PortfolioApi.Model;
using Refit;

namespace PortfolioApi.Apis;

public interface ISuggestAdminApi
{
    [Get("/admin/stats")]
    public Task<SGBackendStats> GetStats();
}

public class SGBackendStats
{
    public long UserMinutes { get; set; }
    
    public long Users { get; set; }
}
