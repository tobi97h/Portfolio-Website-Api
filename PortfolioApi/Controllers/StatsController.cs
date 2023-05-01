using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using PortfolioApi.Model;

namespace PortfolioApi.Controllers
{
    [ApiController]
    [Route("stats")]
    public class StatsController : ControllerBase
    {
        private readonly SingletonStatsProvider _statsProvider;
        
        public StatsController(SingletonStatsProvider statsProvider)
        {
            _statsProvider = statsProvider;
        }

        [HttpGet("stats")]
        public async Task<Stats> GetStats()
        {
            return _statsProvider.GetStats();
        }
    }
}