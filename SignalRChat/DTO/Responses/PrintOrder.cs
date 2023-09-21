namespace SignalRChat.DTO.Responses
{
    public class PrintOrder
    {
        public PrintOrder()
        {
            ArticleList = new List<Article>();
        }
        public int? Id { get; set; }
        public string? Name { get; set; }
        public List<Article> ArticleList { get; set; }

        public override string ToString()
        {
            return "{" +
                $"\"Id\": {this.Id}," +
                $"\"Name\":  \"{this.Name}\"," +
                $"\"ArticleList\": [{String.Join(",", this.ArticleList)}]" +
                "}";

        }

        public Dictionary<string, dynamic> ToDictionary()
        {
            Dictionary<string, dynamic> dict = new()
            {
                ["Id"] = Id,
                ["Name"] = Name,
                ["ArticleList"] = new List<Dictionary<string, dynamic>>()
            };
            foreach (Article art in ArticleList)
            {
                dict["ArticleList"].Add(art.ToDictionary());
            }
            return dict;
        }
    }
}
