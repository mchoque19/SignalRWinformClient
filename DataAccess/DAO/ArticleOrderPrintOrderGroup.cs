using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class ArticleOrderPrintOrderGroup
    {
        public int Id { get; set; }
        public Article Article { get; set; }
        public OrderPrintOrderGroup OrderPrintOrderGroup { get; set; }
    }
}
