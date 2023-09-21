namespace SignalRChat.DTO.Requests
{
    public class PrintOrder
    {
        public int Id { get; set; }
        public List<Article> ArticleList { get; set; }

        public override string ToString()
        {
            return "{" +
                $"\"Id\": {this.Id}," +
                $"\"ArticleList\": {String.Join(",", this.ArticleList)}" +
                "}";

        }
    }
}
