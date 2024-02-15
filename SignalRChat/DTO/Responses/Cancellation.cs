using DAL.Models;

namespace SignalRChat.DTO.Responses
{
    internal class Cancellation
    {
        public int OrderId { get; set; }
        public List<ItemCancelation> CancellationList { get; set; } = new List<ItemCancelation>();
    }

    public class ItemCancelation
    {
        public int OrderLineNo { get; set; }
        public float Units { get; set; }
        public bool Confirmed { get; set; }
    }
}