using DAL.Models;
using DAL.Repositories;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using SignalRChat.DTO;

namespace SignalRChat.Services
{
    public class OrderItemService
    {
        private readonly OrderItemRepository _orderItemRepo;
        private readonly IGenericCRUD<Order> _orderRepo;
        private readonly IGenericCRUD<State> _stateRepo;

        public OrderItemService(OrderItemRepository orderItemRepo, IGenericCRUD<Order> orderRepo, IGenericCRUD<State> stateRepo)
        {
            _orderItemRepo = orderItemRepo;
            _orderRepo = orderRepo;
            _stateRepo = stateRepo;
        }



        //private readonly DAL.Services.OrderItemService _orderItemService;
        //private readonly DAL.Services.KitchenOrderService _kitchenOrderService;
        //private readonly DAL.Services.StateService _stateService;

        public void ChangeStatus(ChangeStatus changeStatus)
        {
            Order? orderDb = _orderRepo.GetById(changeStatus.OrderId);
            if (orderDb != null)
            {
                foreach(Change change in changeStatus.ChangeList) 
                {
                    OrderItem? orderItemDb = _orderItemRepo.GetByIdAndLine(orderDb.Id, change.OrderLineNo);
                    if (orderItemDb != null)
                    {
                        State? oldState = _stateRepo.GetById(change.OldState);
                        if (orderItemDb.State == oldState)
                        {
                            State? newState = _stateRepo.GetById(change.NewState);
                            orderItemDb.State = newState;
                            _orderItemRepo.Update(orderItemDb);
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }
                
                }
            }
            else
            {

            }
        }
     
        public OrderItem FindByArticleDescription(int orderId, int articleId, List<string> ModifList, int? printOrderId)
        {
            return _orderItemRepo.GetByArticleDescription(orderId, articleId, ModifList, printOrderId);
        }

       
    }

}
