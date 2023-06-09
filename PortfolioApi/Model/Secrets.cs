namespace PortfolioApi.Model;

public class Secrets
{
    public string GithubToken { get; set; }
    
    public string GithubUser { get; set; }

    public string ValidSourceCodeFiles { get; set; }
    
    public string GhostToken { get; set; }
    
    public string? GhostUrl { get; set; }
    
    public string? DroneToken { get; set; }
}