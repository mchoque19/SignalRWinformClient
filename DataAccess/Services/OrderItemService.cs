using DAL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class OrderItemService
    {
        private readonly KitchenServerDbContext _context;

        public OrderItemService(KitchenServerDbContext context)
        {
            _context = context;
        }

        public OrderItem? GetById(int orderId, int orderLineNo)
        {
            return _context.OrderItem.Where(x=> x.OrderId == orderId && x.OrderLineNo == orderLineNo).FirstOrDefault();
        }

        public void ChangeStatus(Dictionary<String, dynamic> changeStatus)
        {
            string orderId = changeStatus["OrderId"];
            int termNo = changeStatus["TermNo"];
            Order? orderDb = _context.Order.Where(x=> x.OrderId == orderId && x.TermNo == termNo).FirstOrDefault();
            foreach (var change in changeStatus["ChangeList"]) 
            {
                OrderItem orderItemDb = GetById(orderDb.Id, change["OrderLineNo"]);
                if (orderDb == null)
                {
                    throw new Exception("La comanda no existe");
                }
                else
                {
                    if (orderItemDb.State.Id != change["OldState"])
                    {
                        throw new Exception("Esta comanda ya ha sido actualizada");
                    }
                    else
                    {
                        State newState = _context.State.Find(change["NewState"]);
                        orderItemDb.State = newState;
                        _context.SaveChanges();
                    }
                }
            }
        }

        public bool IsOrderFinished(string orderId, int termNo)
        {
            // TODO: Comprobar que la comanda esta pagada
            Order orderDb = _context.Order.Where(x=> x.OrderId == orderId && x.TermNo == termNo).FirstOrDefault();
            List<OrderItem> itemList = _context.OrderItem.Where(x => x.OrderId == orderDb.Id).ToList();
            State lastState = _context.State.OrderBy(x => x.Order).First();
            Console.WriteLine(lastState.Name);
            if (orderDb == null)
            {
                throw new Exception("No existe la comanda");
            }
            else
            {
                if (itemList.FindAll(x => x.State != lastState).Count() > 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
