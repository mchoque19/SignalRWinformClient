using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Cancellation
    {
        private int id;
        private int orderId;
        private int orderLineNo;
        private float units;
        private DateTime? confirmed;
        private DateTime datetime = DateTime.Now;

        public int OrderId { get => orderId; set => orderId = value; }
        public int OrderLineNo { get => orderLineNo; set => orderLineNo = value; }
        public float Units { get => units; set => units = value; }
        public DateTime? Confirmed { get => confirmed; set => confirmed = value; }
        public DateTime Datetime { get => datetime; set => datetime = value; }

        public virtual OrderItem OrderItem { get; set; }
        public int Id { get => id; set => id = value; }
    }
}
