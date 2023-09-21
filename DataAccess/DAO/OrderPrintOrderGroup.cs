
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class OrderPrintOrderGroup
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public PrintOrderGroup PrintOrderGroup { get; set; }
        public ICollection<ArticleOrderPrintOrderGroup> ArticleOrderPrintOrderGroupList { get; set; }
    }
}
