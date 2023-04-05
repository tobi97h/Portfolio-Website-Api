namespace PortfolioApi.Model
{
    public class GhostPostsResponse
    {
        public List<Post> posts { get; set; }
        public Meta meta { get; set; }
    }

    public class Meta
    {
        public Pagination pagination { get; set; }
    }

    public class Pagination
    {
        public int page { get; set; }
        public int limit { get; set; }
        public int pages { get; set; }
        public int total { get; set; }
        public int next { get; set; }
        public object prev { get; set; }
    }

    public class Post
    {
        public string id { get; set; }
        public string uuid { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
        public string html { get; set; }
        public string comment_id { get; set; }
        public object feature_image { get; set; }
        public bool featured { get; set; }
        public string visibility { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime published_at { get; set; }
        public object custom_excerpt { get; set; }
        public object codeinjection_head { get; set; }
        public object codeinjection_foot { get; set; }
        public object custom_template { get; set; }
        public object canonical_url { get; set; }
        public string url { get; set; }
        public string excerpt { get; set; }
        public int reading_time { get; set; }
        public bool access { get; set; }
        public bool comments { get; set; }
        public object og_image { get; set; }
        public object og_title { get; set; }
        public object og_description { get; set; }
        public object twitter_image { get; set; }
        public object twitter_title { get; set; }
        public object twitter_description { get; set; }
        public object meta_title { get; set; }
        public object meta_description { get; set; }
        public object email_subject { get; set; }
        public object frontmatter { get; set; }
        public object feature_image_alt { get; set; }
        public object feature_image_caption { get; set; }
    }
}


