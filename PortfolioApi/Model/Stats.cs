namespace PortfolioApi.Model;

public class Stats
{
    public long commits { get; set; }
    
    public long repos { get; set; }
    
    public long linesOfCode { get; set; }
    
    public long ghostBlogEntries { get; set; }
    
    public long executedBuilds { get; set; }
    
    public long suggestMinutes { get; set; }
    
    public long suggestUsers { get; set; }
}