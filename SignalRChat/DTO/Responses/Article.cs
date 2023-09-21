namespace SignalRChat.DTO.Responses
{
    public class Article
    {
        public Article()
        {
            MonitorList = new List<uint>();
            ModifList = new List<string>();
        }
        public int Id { get; set; }
        public int OrderLineNo { get; set; }
        public string Name { get; set; }
        public int Units { get; set; }
        public List<uint> MonitorList { get; set; }
        public List<string> ModifList { get; set; }
        public Menu? Menu { get; set; }
        public int StateId { get; set; } = 1;

        public override string ToString()
        {
            return "{" +
                $"\"Id\":{this.Id}, " +
                $"\"OrderLineNo\":{this.OrderLineNo}, " +
                $"\"Name\":  \"{this.Name} \", " +
                $"\"Units\": {this.Units}, " +
                $"\"ModifList\": [{String.Join("\", \"", this.ModifList)}], " +
                $"\"MonitorList\": [{String.Join(", ", this.MonitorList)}] " +
                $"\"Menu\": {Menu ?? null}" +
                "}";

        }

        public Dictionary<string,dynamic> ToDictionary()
        {
            Dictionary<string, dynamic> dict = new()
            {
                ["Id"] = Id,
                ["Name"] = Name,
                ["OrderLineNo"] = OrderLineNo,
                ["Units"] = Units,
                ["ModifList"] = String.Join(", ",ModifList),
                ["MonitorList"] = MonitorList,
                ["Menu"] = Menu?.ToDictionary(),
                ["StateId"]= StateId
            };
            return dict;
        }
    }
}
