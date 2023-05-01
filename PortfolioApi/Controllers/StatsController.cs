using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using PortfolioApi.Model;

namespace PortfolioApi.Controllers
{
    [ApiController]
    [Route("stats")]
    public class StatsController : ControllerBase
    {
        private readonly MySqlConnection _mySqlConnection;
        
        private readonly SingletonStatsProvider _statsProvider;
        
        public StatsController(MySqlConnection mySqlConnection, SingletonStatsProvider statsProvider)
        {
            _mySqlConnection = mySqlConnection;
            _statsProvider = statsProvider;
        }

        [HttpGet("stats")]
        public async Task<Stats> GetStats()
        {
            return _statsProvider.GetStats();
        }
    }
}