namespace PortfolioApi;

public class Repo
{
    public string name { get; set; }
    
    public string full_name { get; set; }
    
    public Owner owner { get; set; }
}

public class Owner
{
    public string login { get; set; }
}