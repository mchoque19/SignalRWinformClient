namespace SignalRChat.DTO.Responses
{
    public class Menu
    {

        public Menu()
        {
            printOrderlist = new List<PrintOrder>();
        }
        public string Name { get; set; }
        public List<PrintOrder> printOrderlist { get; set; }

        public override string ToString()
        {
            return "{" +
               $"\"Name\": {Name}, " +
               $"\"PrintOrderList\":{String.Join("\", \"", printOrderlist)}" +
               "}";
        }

        public Dictionary<string, dynamic> ToDictionary()
        {
            Dictionary<string, dynamic> dict = new()
            {
                ["Name"] = Name,
                ["PrintOrderList"] = new List<Dictionary<string, dynamic>>()
            };

            foreach (PrintOrder printOrder in printOrderlist)
            {
                dict["PrintOrderList"].Add(printOrder.ToDictionary());
            }
            return dict;
        }
    }
}
