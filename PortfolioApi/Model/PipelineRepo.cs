namespace PortfolioApi.Model;

public class PipelineRepo
{
    public int id { get; set; }
    public string uid { get; set; }
    public int user_id { get; set; }
    public string @namespace { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
    public string scm { get; set; }
    public string git_http_url { get; set; }
    public string git_ssh_url { get; set; }
    public string link { get; set; }
    public string default_branch { get; set; }
    public bool @private { get; set; }
    public string visibility { get; set; }
    public bool active { get; set; }
    public string config_path { get; set; }
    public bool trusted { get; set; }
    public bool @protected { get; set; }
    public bool ignore_forks { get; set; }
    public bool ignore_pull_requests { get; set; }
    public bool auto_cancel_pull_requests { get; set; }
    public bool auto_cancel_pushes { get; set; }
    public bool auto_cancel_running { get; set; }
    public int timeout { get; set; }
    public int counter { get; set; }
    public int synced { get; set; }
    public int created { get; set; }
    public int updated { get; set; }
    public int version { get; set; }
    public bool archived { get; set; }
}
