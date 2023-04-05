namespace PortfolioApi.Model;


public class Commit
{
    public Author author { get; set; }
}

public class Author
{
    public string login { get; set; }
}