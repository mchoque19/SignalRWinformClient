using Microsoft.Identity.Client.Extensions.Msal;
using System.Threading.Channels;

namespace SignalRChat.DTO
{
    public class ChangeStatus
    {
        public string SoftwareVers { get; set; }
        public int MadiCustNo { get; set; }
        public int CompNo { get; set; }
        public int StoreNo { get; set; }
        public int TermNo { get; set; }
        public int OperNo { get; set; }
        public string OperName { get; set; }
        public string? DateTime { get; set; }
        public int OrderId { get; set; }
        public List<Change> ChangeList { get; set; }

        public Dictionary<string, dynamic> ToDictionary()
        {
            Dictionary<string, dynamic> dict = new()
            {
                ["SoftwareVers"] = SoftwareVers,
                ["MadiCustNo"] = MadiCustNo,
                ["CompNo"] = CompNo,
                ["StoreNo"] = StoreNo,
                ["TermNo"] = TermNo,
                ["OperNo"] = OperNo,
                ["OperName"] = OperName,
                ["DateTime"] = DateTime,
                ["OrderId"] = OrderId,
                ["ChangeList"] = new List<Dictionary<string, dynamic>>()
            };

            foreach (Change c in ChangeList)
            {
                dict["ChangeList"].Add(c.ToDictionary());
            }
            return dict;
        }


    }

    public class Change
    {
        public int OrderLineNo { get; set; }
        public int OldState { get; set; }
        public int NewState { get; set; }

        public Dictionary<string, dynamic> ToDictionary()
        {
            Dictionary<string, dynamic> dict = new()
            {
                ["OrderLineNo"] = OrderLineNo,
                ["OldState"] = OldState,
                ["NewState"] = NewState

            };
            return dict;
        }
    }
}
