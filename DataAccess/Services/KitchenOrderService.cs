using DAL.DAO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class KitchenOrderService
    {
        private readonly KitchenServerDbContext _context;
        public KitchenOrderService(KitchenServerDbContext context)
        {
            _context = context;
        }
        public void save(Dictionary<string,dynamic> order)
        {
            Order orderdb = new()
            {
                SoftwareVers = order["SoftwareVers"],
                MadiCustNo = order["MadiCustNo"],
                CompNo = order["CompNo"],
                StoreNo = order["StoreNo"],
                OrderId = order["OrderId"],
                TermNo = order["TermNo"],
                OperNo = order["OperNo"],
                OperName = order["OperName"],
                TbNum = order["TbNum"],
                Pax = order["Pax"],
                TableType = order["TableType"],
                StartTime = DateTime.Parse(order["StartTime"])
            };
            _context.Order.Add(orderdb);
            _context.SaveChanges();
            foreach (var po in order["PrintOrderList"]) 
            {
                Console.WriteLine("Fuera del menu");
                Console.WriteLine(po.ToString());

                populateOrderItem(po, orderdb.Id, null);
            
            }
            
        }

        private void populateOrderItem(Dictionary<string,dynamic> printOrder, int orderId, int? MenuOrderLine)
        {
            
                PrintOrderGroup? poDb = null;
                if (printOrder["Id"] != null)
                {
                    poDb = _context.Find<PrintOrderGroup>(printOrder["Id"]);
                }
                foreach (var art in printOrder["ArticleList"])
                {
                    Article artDb = _context.Find<Article>(art["Id"]);
                    OrderItem item = new()
                    {
                        
                        
                        OrderId = orderId,
                        OrderLineNo = art["OrderLineNo"],
                      
                        PrintOrderGroup = poDb ?? null,
                        Article = artDb,
                        Units = art["Units"],
                        ModifList = art["ModifList"],
                        MenuOrderLineNo = MenuOrderLine,
                        State = _context.Find<State>(art["StateId"]),
                    };
                    _context.OrderItem.Add(item);
                    _context.SaveChanges();
                    
                    if (art["Menu"] != null)
                    {
                        foreach (var po in art["Menu"]["PrintOrderList"])
                        {
                            Console.WriteLine("Dentro");
                            Console.WriteLine(po.ToString());
                            populateOrderItem(po, orderId, art["OrderLineNo"]);
                        }
                    }
                }
            
        }
    }
}
