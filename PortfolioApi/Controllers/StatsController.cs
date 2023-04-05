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
        
        [HttpGet("suggest/minutes")]
        public async Task<long> GetSuggestMinutes()
        {
            await _mySqlConnection.OpenAsync();
            using var command = new MySqlCommand("SELECT TotalSeconds FROM PlaybackSummaries;",_mySqlConnection);
            using var reader = await command.ExecuteReaderAsync();
            var totalMinutes = 0l;
            while (await reader.ReadAsync())
            {
                var minutes = reader.GetInt64("TotalSeconds") / 60;
                totalMinutes += minutes;
            }
            return totalMinutes;
        }
        
        [HttpGet("suggest/users")]
        public async Task<long> GetSuggestUsers()
        {
            await _mySqlConnection.OpenAsync();
            using var command = new MySqlCommand("SELECT count(*) FROM User;",_mySqlConnection);
            using var reader = await command.ExecuteReaderAsync();
            var totalMinutes = 0l;
            if (await reader.ReadAsync())
            {
                return reader.GetInt64(0);
             
            }
            return 0;
        }
    }
}