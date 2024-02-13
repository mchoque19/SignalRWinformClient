using DAL.Services;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis.CSharp.Syntax;
namespace SignalRChat.DTO.Requests
{
    public class Article
    {
        public int Id { get; set; }
        public float Units { get; set; }
        public List<string> ModifList { get; set; }
        public Menu? Menu { get; set; }

        public override string ToString()
        {
            return "{" +
                $"\"Id\": {this.Id}," +
                $"\"Units\": {this.Units}," +
                //$"\"Menu\": {Menu ?? null}" +
                "}";
        }

    }
}
