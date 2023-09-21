namespace SignalRChat.DTO.Responses
{
    public class KitchenOrder
    {
        public KitchenOrder()
        {
            PrintOrderList = new List<PrintOrder>();
        }

        public string SoftwareVers { get; set; }
        public string OrderId { get; set; }
        public int MadiCustNo { get; set; }
        public int CompNo { get; set; }
        public int StoreNo { get; set; }
        public int TermNo { get; set; }
        public int OperNo { get; set; }
        public string OperName { get; set; }
        public string StartTime { get; set; }
        public string TbNum { get; set; }
        public int Pax { get; set; }
        public string TableType { get; set; }
        public string? RefOrderId { get; set; }
        public List<PrintOrder> PrintOrderList { get; set; }

        public override string ToString()
        {
            return "{" +
                $"\"SofwareVers\":  \"{this.SoftwareVers}\"," +
                $"\"MadiCustNo\": {this.MadiCustNo}," +
                $"\"CompNo\": {this.CompNo}," +
                $"\"StoreNo\": {this.StoreNo}," +
                $"\"TermNo\": {this.TermNo}," +
                $"\"OperNo\": {this.OperNo}," +
                $"\"OperName\":  \"{this.OperName} \"," +
                $"\"StartTime\":  \"{this.StartTime} \"," +
                $"\"TbNum\":  \"{this.TbNum} \"," +
                $"\"Pax\": {this.Pax}," +
                $"\"TableType\":  \"{this.TableType} \"," +
                $"\"RefOrderId\": {this.RefOrderId?? "null"}," +
                $"\"PrintOrderList\": [{String.Join(",", this.PrintOrderList)}]" +
                "}";

        }
        public Dictionary<string, dynamic> ToDictionary()
        {
            Dictionary<string, dynamic> dict = new()
            {
                ["SoftwareVers"] = SoftwareVers,
                ["OrderId"] = OrderId,
                ["MadiCustNo"] = MadiCustNo,
                ["CompNo"] = CompNo,
                ["StoreNo"] = StoreNo,
                ["TermNo"] = TermNo,
                ["OperNo"] = OperNo,
                ["OperName"] = OperName,
                ["StartTime"] = StartTime,
                ["TbNum"] = TbNum,
                ["Pax"] = Pax,
                ["TableType"] = TableType,
                ["RefOrderId"] = RefOrderId,
                ["PrintOrderList"] = new List<Dictionary<string, dynamic>>()
            };

            foreach (PrintOrder po in PrintOrderList)
            {
                dict["PrintOrderList"].Add(po.ToDictionary());
            }
            return dict;
        }
    }
}
