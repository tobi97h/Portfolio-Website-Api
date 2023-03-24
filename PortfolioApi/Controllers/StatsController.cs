using System.Diagnostics;
using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace PortfolioApi.Controllers
{
    [ApiController]
    [Route("stats")]
    public class StatsController : ControllerBase
    {
        private readonly IGithubApi _githubApi;

        private readonly MySqlConnection _mySqlConnection;

        private readonly Logger<StatsController> _logger;

        public StatsController(IGithubApi githubApi, MySqlConnection mySqlConnection, Logger<StatsController> logger)
        {
            _githubApi = githubApi;
            _mySqlConnection = mySqlConnection;
            _logger = logger;
        }

        [HttpGet("stats")]
        public async Task<Stats> GetStats()
        {
            var repos = await _githubApi.GetUserRepos();
            // download and count lines
            Directory.CreateDirectory("tempRepos");
            foreach (var repo in repos)
            {
                //var zip = await _githubApi.DownloadRepo(repo.owner.login, repo.name);
                //var archive = new ZipArchive(await zip.Content.ReadAsStreamAsync());
                //archive.ExtractToDirectory("tempRepos");
            }
            // iterate and run code to get lines of code
            foreach (var repo in Directory.GetDirectories("tempRepos"))
            {
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "bash",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        WorkingDirectory = repo
                    }
                };
                process.Start();
                await process.StandardInput.WriteLineAsync("git ls-files");
                var output = await process.StandardOutput.ReadLineAsync();
                _logger.LogError(output);
            }

            return new Stats
            {

            };
        }
        
        public async Task<long> GetLinesOfCode()
        {
            var repos = await _githubApi.GetUserRepos();

            return 0l;
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
                return reader.GetInt64(0) ;
             
            }
            return 0;
        }

        [HttpGet("repos")]
        public async Task<long> GetRepos()
        {
            return (await _githubApi.GetUserRepos()).Length;
        }

        [HttpGet("commits")]
        public async Task<long> GetCommits()
        {
            var repos = await _githubApi.GetUserRepos();
            var commits = 0l;
            foreach (var repo in repos)
            {
                var resp = await _githubApi.GetCommits(repo.owner.login, repo.name);
                resp.Headers.TryGetValues("Link", out var link);
                if (link != null && link.Any())
                {
                    var value = link.First();
                    var lastIndex = value.LastIndexOf("page=");
                    var commitsInRepo = long.Parse(value.Substring(lastIndex + "page=".Length).Split(">")[0]);
                    commits += commitsInRepo;
                }
            }
            
            return commits;
        }
    }
}