using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using DAL.Models;
using SignalRChat.Services;
using DAL.Repositories;
using SignalRChat.DTO;
using System.Transactions;


namespace SignalRChat.Hubs
{
	[Authorize]
	public class ChatHub: Hub
    {
        private readonly OrderService _orderService;
        private readonly OrderItemService _orderItemService;
        private readonly OrderRepository _orderRepo;
        private readonly CancellationService _cancellationSerivce;
        private readonly IGenericCRUD<State> _stateRepo;
        private readonly IGenericCRUD<PrintOrderGroup> _printOrderRepo;
        //private readonly IService<PrintOrderGroup> _printOrderGroupService;
        //private readonly ArticleService _articleRepo;
        //private readonly OrderItemService _orderItemService;


        //private readonly IGenericCRUD<Order> _orderRepo;

        public ChatHub(OrderService orderService, OrderItemService orderItemService, OrderRepository orderRepo, CancellationService cancellationService, IGenericCRUD<State> stateRepo, IGenericCRUD<PrintOrderGroup> printOrderRepo)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _orderRepo = orderRepo;
            _cancellationSerivce = cancellationService;
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
            Console.WriteLine(json.ToString());
            _orderItemService.ChangeStatus(json);
            Console.WriteLine("Enviando cambio de estado");

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

        public async Task FireOrder(DTO.Requests.FireOrder json)
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
                await Clients.All.SendAsync("ChangeStatus", changeStatus);
            }
            else
            {

            }

        }

        public async Task CloseTable(DTO.Requests.CloseTable json)
        {
            Order? order = _orderRepo.Find(json.SoftwareVers, json.MadiCustNo, json.CompNo, json.StoreNo, json.TermNo, json.TransNo);
            if (order != null)
            {

                if (_orderService.IsOrderFinished(order.Id))
                {
                    DTO.Responses.CloseTable respones = new()
                    {
                        OrderId = order.Id
                    };
                    await Clients.All.SendAsync("CloseTable", respones);
                }
            }
        }

        public async Task Transfer(DTO.Requests.Transfer json)
        {
            DTO.Requests.KitchenOrder newOrder = new()
            {
                SoftwareVers    = json.SoftwareVers,
                MadiCustNo      = json.MadiCustNo,
                CompNo          = json.CompNo,
                StoreNo         = json.StoreNo,
                TermNo          = json.TermNo,
                TransNo         = json.TransNo,
                OperNo          = json.OperNo,
                OperName        = json.OperName,
                TbNum           = json.TbNum,
                Pax             = json.Pax,
                TableType       = json.TableType,
            };
            foreach (DTO.Requests.Cancellation cancellation in json.VoidOrderKitchen)
            {
                cancellation.TransferOperation = true;
                VoidOrder(cancellation);
                foreach(DTO.Requests.PrintOrder po in cancellation.PrintOrderList)
                {
                    foreach(DTO.Requests.Article article in po.ArticleList)
                    {
                        article.Units = Math.Abs(article.Units);
                    }
                    newOrder.PrintOrderList.Add(po);
                }
            }
            KitchenOrder(newOrder);
        }

        public async Task VoidOrder(DTO.Requests.Cancellation json)
        {
            Order? order = _orderRepo.Find(json.SoftwareVers, json.MadiCustNo, json.CompNo, json.StoreNo, json.TermNo, json.TransNo);

            if (order != null)
            {
                DTO.Responses.Cancellation result = new()
                {
                    OrderId = order.Id,
                };
                foreach (DTO.Requests.PrintOrder po in json.PrintOrderList)
                {
                    foreach(DTO.Requests.Article a in po.ArticleList)
                    {

                        OrderItem? oItem = _orderItemService.FindByArticleDescription(order.Id, a.Id, a.ModifList, po.Id);
                        if (oItem != null)
                        {
                            DAL.Models.Cancellation? cancellation = _cancellationSerivce.NewCancellation(oItem, a.Units, json.TransferOperation);
                            if (cancellation != null)
                            {
                                DTO.Responses.ItemCancelation itemCancellation = new()
                                {
                                    OrderLineNo = cancellation.OrderLineNo,
                                    Units = cancellation.Units,
                                    Confirmed = cancellation.Confirmed != null
                                };
                                result.CancellationList.Add(itemCancellation);
                            }
                        }
                        else
                        {

                        }
                       
                    }
                }
                await Clients.All.SendAsync("Cancellation", result);
            }
        }
    }
}
