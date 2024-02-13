using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace DAL.Models
{
    public class OrderItem
    {
        private int orderId;
        private int orderLineNo;
        private float units;
        private string modifList;
        private int? menuOrderLineNo;


        public int OrderId { get => orderId; set => orderId = value; }
        public int OrderLineNo { get => orderLineNo; set => orderLineNo = value; }
        public float Units { get => units; set => units = value; }
        public string ModifList { get => modifList; set => modifList = value; }
        public int? MenuOrderLineNo { get => menuOrderLineNo; set => menuOrderLineNo = value; }


        public virtual PrintOrderGroup? PrintOrderGroup { get; set; }
        public virtual Article Article { get; set; }
        public virtual State State { get; set; }
        public virtual Order Order { get; set; }

    }
}
