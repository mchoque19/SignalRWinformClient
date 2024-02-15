namespace SignalRChat.DTO.Requests
{
    public class Transfer
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
        public List<Cancellation> VoidOrderKitchen { get; set; }
    }
}