namespace SignalRChat.DTO.Requests
{
    public class KitchenOrder
    {
        public string SoftwareVers { get; set; }
        public int MadiCustNo { get; set; }
        public int CompNo { get; set; }
        public int StoreNo { get; set; }
        public long TransNo { get; set; }
        public int TermNo { get; set; }
        public int OperNo { get; set; }
        public string OperName { get; set; }
        public string? TbNum { get; set; }
        public int Pax { get; set; }
        public string TableType { get; set; }
        //public string? RefOrderId { get; set; }
        public List<PrintOrder> PrintOrderList { get; set; } = new List<PrintOrder>();

        public override string ToString()
        {
            return "{" +
                $"\"SoftwareVers\": {this.SoftwareVers}," +
                $"\"MadiCustNo\": {this.MadiCustNo}," +
                $"\"CompNo\": {this.CompNo}," +
                $"\"StoreNo\": {this.StoreNo}," +
                $"\"TermNo\": {this.TermNo}," +
                $"\"OperNo\": {this.OperNo}," +
                $"\"OperName\": {this.OperName}," +
                //$"\"StartTime\": {this.StartTime}," +
                $"\"TbNum\": {this.TbNum}," +
                $"\"Pax\": {this.Pax}," +
                $"\"TableType\": {this.TableType}," +
                //$"\"RefOrderId\": {this.RefOrderId}," +
                $"\"PrintOrderList\": {String.Join(",", this.PrintOrderList)}" +
                "}";
        }

        
    }
}
