namespace SignalRChat.DTO.Requests
{
    public class Cancellation
    {
        public string SoftwareVers { get; set; }
        public int MadiCustNo { get; set; }
        public int CompNo { get; set; }
        public int StoreNo { get; set; }
        public long TransNo { get; set; }
        public int TermNo { get; set; }
        public int OperNo { get; set; }
        public bool TransferOperation { get; set; } = false;
        public List<PrintOrder> PrintOrderList { get; set; }
    }
}