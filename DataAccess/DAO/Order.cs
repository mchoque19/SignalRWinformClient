using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string SoftwareVers{ get; set; }
        
        public int MadiCustNo{ get; set; }
        
        public int CompNo{ get; set; }
        
        public int StoreNo{ get; set; }
        
        public string OrderId{ get; set; }
        
        public int? TermNo { get; set; }
        public int? OperNo { get; set; }
        public string? OperName { get; set; }
        public string? TbNum { get; set; }
        public int Pax { get; set; }
        public string TableType { get; set; }
        public DateTime StartTime { get; set; }
        //public virtual List<OrderItem> Items { get; set; }

    }
}
