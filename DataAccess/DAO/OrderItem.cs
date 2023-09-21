using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace DAL.DAO
{
    public class OrderItem
    {
        [Key]
        public int OrderId{ get; set; }
        [Key]
        public int OrderLineNo{ get; set; }
        public  virtual PrintOrderGroup? PrintOrderGroup{ get; set; }
        public  virtual Article Article{ get; set; }
        public int Units{ get; set; }
        public string ModifList{ get; set; }
        public int? MenuOrderLineNo{ get; set; }
        public virtual State State { get; set; }
         


    }
}
