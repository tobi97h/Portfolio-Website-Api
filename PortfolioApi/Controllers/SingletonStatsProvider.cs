﻿using System.IO.Compression;
using SecretsProvider;

namespace PortfolioApi.Controllers;


/// <summary>
/// needs to be registered as singleton, fetches lines of code once on startup, refreshed weekly by server container restart
/// </summary>
public class SingletonStatsProvider
{
    private readonly IServiceScopeFactory _scopeFactory;
    
    private readonly Stats _stats;

    public SingletonStatsProvider(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        _stats = CalcStats().Result;
    }

    private async Task<Stats> CalcStats()
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var githubApi = scope.ServiceProvider.GetRequiredService<IGithubApi>();
            var secretsProvider = scope.ServiceProvider.GetRequiredService<ISecretsProvider>();
            var secrets = secretsProvider.GetSecret<Secrets>();
            
            // ghost stats 
            var ghostApi = scope.ServiceProvider.GetRequiredService<IGhostApi>();
            var postsResponse = await ghostApi.GetPosts(secrets.GhostToken);
            
            
            
            var repos = githubApi.GetUserRepos().Result;
            
            // calc lines of code
            var repoFolder = "tempRepos";
            Directory.Delete(repoFolder, true);
            Directory.CreateDirectory(repoFolder);
            foreach (var repo in repos)
            {
                var zip = await githubApi.DownloadRepo(repo.owner.login, repo.name);
                var archive = new ZipArchive(await zip.Content.ReadAsStreamAsync());
                archive.ExtractToDirectory("tempRepos");
            }

            long totalLines = 0;
            var validSourceCodeEndings = secrets.ValidSourceCodeFiles.Split(",");
            // iterate and run code to get lines of code
            foreach (var repo in Directory.GetDirectories("tempRepos"))
            {
                foreach (string file in Directory.EnumerateFiles(repo, "*.*", SearchOption.AllDirectories))
                {
                    if (validSourceCodeEndings.Any(ending => file.EndsWith(ending)))
                    {
                        var lines = System.IO.File.ReadAllLines(file);
                        totalLines += lines.Length;
                    }
                }
            }
            
            // calc total commits
            var commits = 0l;
            foreach (var repo in repos)
            {
                var resp = await githubApi.GetCommits(repo.owner.login, repo.name);
                resp.Headers.TryGetValues("Link", out var link);
                if (link != null && link.Any())
                {
                    var value = link.First();
                    var lastIndex = value.LastIndexOf("page=");
                    var commitsInRepo = long.Parse(value.Substring(lastIndex + "page=".Length).Split(">")[0]);
                    commits += commitsInRepo;
                }
            }
            
            return new Stats
            {
                commits = commits,
                repos = repos.Length,
                linesOfCode = totalLines,
                ghostBlogEntries = postsResponse.meta.pagination.total
            };
        }
    }

    public Stats GetStats()
    {
        return _stats;
    }
    
}