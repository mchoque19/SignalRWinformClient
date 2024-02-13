using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using DAL.Models;
using SignalRChat.Services;
using DAL.Repositories;
using SignalRChat.DTO;


namespace SignalRChat.Hubs
{
	[Authorize]
	public class ChatHub: Hub
    {
        private readonly OrderService _orderService;
        private readonly OrderItemService _orderItemService;
        private readonly OrderRepository _orderRepo;
        private readonly IGenericCRUD<State> _stateRepo;
        private readonly IGenericCRUD<PrintOrderGroup> _printOrderRepo;
        //private readonly IService<PrintOrderGroup> _printOrderGroupService;
        //private readonly ArticleService _articleRepo;
        //private readonly OrderItemService _orderItemService;


        //private readonly IGenericCRUD<Order> _orderRepo;

        public ChatHub(OrderService orderService, OrderItemService orderItemService, OrderRepository orderRepo, IGenericCRUD<State> stateRepo, IGenericCRUD<PrintOrderGroup> printOrderRepo)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _orderRepo = orderRepo;
            _stateRepo = stateRepo;
            _printOrderRepo = printOrderRepo;
        }

        public async Task KitchenOrder(/*Dictionary<string, dynamic> json)*/ DTO.Requests.KitchenOrder json)
        {
            Console.WriteLine(json);
            DTO.Responses.KitchenOrder response = _orderService.New(json);

            Console.WriteLine(response.ToString());
            await Clients.All.SendAsync("KitchenOrder", response);
        }

        public async Task ChangeStatus(DTO.ChangeStatus json)
        {
            // TODO: comprobar que el OldStatus coincide con el de BBDD y hacer el cambio
            Console.WriteLine("Cambio de estado");
            _orderItemService.ChangeStatus(json);
            await Clients.All.SendAsync("ChangeStatus", json);
            if (_orderService.IsOrderFinished(json.OrderId))
            {
                DTO.Responses.CloseTable closeTableDto = new()
                {
                    OrderId = json.OrderId,
                };
                await Clients.All.SendAsync("CloseTable", closeTableDto);
            }
        }


        public async Task Marchando(DTO.Requests.Marhcando json)
        {
            PrintOrderGroup printOrderGroup = _printOrderRepo.GetById(json.PrintOrderId);
            Order? orderDb = _orderRepo.Find(json.SoftwareVers, json.MadiCustNo, json.CompNo, json.StoreNo, json.TermNo, json.TransNo);
            State state = _stateRepo.GetAll().Where(s => s.Marchando).First();
            if (orderDb != null)
            {
                DTO.ChangeStatus changeStatus = new()
                {
                    SoftwareVers = orderDb.SoftwareVers,
                    MadiCustNo = orderDb.MadiCustNo,
                    CompNo = orderDb.CompNo,
                    StoreNo = orderDb.StoreNo,
                    TermNo = orderDb.TermNo,
                    OrderId = orderDb.Id,
                    DateTime = DateTime.Now.ToString("u"),
                    ChangeList = new List<Change>()
                        
                };

                List<OrderItem> items = orderDb.OrderItems.Where(oi => oi.PrintOrderGroup == printOrderGroup).ToList();
                foreach(OrderItem orderItem in items)
                {
                    Change change = new()
                    {
                        OrderLineNo = orderItem.OrderLineNo,
                        OldState = orderItem.State.Id,
                        NewState = state.Id,
                    };
                    changeStatus.ChangeList.Add(change);
                }
                _orderItemService.ChangeStatus(changeStatus);
            }
            else
            {

            }
            await Clients.All.SendAsync("ChangeStatus", changeStatus);

        }

        //public async Task CloseTable(DTO.Requests.CloseTable json)
        //{
        //    if (_orderItemService.IsOrderFinished(json.OrderId, json.TermNo))
        //    {
        //        await Clients.All.SendAsync("CloseTable", json);
        //    }
        //}

    }
}
