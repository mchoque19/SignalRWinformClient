
using Microsoft.AspNetCore.SignalR;
using DAL.Services;
using DAL.DAO;
using System.Drawing.Text;
using SignalRChat.DTO.Requests;
using SignalRChat.DTO;
using NuGet.Protocol;

namespace SignalRChat.Hubs
{
    public class ChatHub: Hub
    {
        private readonly KitchenOrderService _orderService;
        private readonly IService<PrintOrderGroup> _printOrderGroupService;
        private readonly IService<DAL.DAO.Article> _articleService;
        private readonly OrderItemService _orderItemService;

        public ChatHub(KitchenOrderService orderService, IService<PrintOrderGroup> printOrderGroupService, IService<DAL.DAO.Article> articleService, OrderItemService orderItemService)
        {
            _orderService = orderService;
            _printOrderGroupService = printOrderGroupService;
            _articleService = articleService;
            _orderItemService = orderItemService;
        }

        public async Task KitchenOrder(DTO.Requests.KitchenOrder json)
        {
            
            int OrderLineNo = 0;
            DTO.Responses.KitchenOrder response = new()
            {
                CompNo = json.CompNo,
                SoftwareVers = json.SoftwareVers,
                MadiCustNo = json.MadiCustNo,
                StoreNo = json.StoreNo,
                OperNo = json.OperNo,
                OrderId = json.OrderId,
                StartTime = json.StartTime,
                OperName = json.OperName,
                TermNo = json.TermNo,
                Pax = json.Pax,
                TbNum = json.TbNum,
                TableType = json.TableType,
                RefOrderId = json.RefOrderId
            };
            foreach (var p in json.PrintOrderList)
            {
                PrintOrderGroup? orderGroup = _printOrderGroupService.GetById(p.Id);
                DTO.Responses.PrintOrder print = new DTO.Responses.PrintOrder();
                
                print.Id =  orderGroup?.Id ?? null ;
                print.Name = orderGroup?.Name ?? null;
                foreach (var a in p.ArticleList)
                {
                    OrderLineNo++;
                    DAL.DAO.Article? articleDB = _articleService.GetById(a.Id);
                    //DTO.Responses.Article resArticle = a.toResponse(articleDB);
                    DTO.Responses.Article resArticle = new()
                    {
                        // TODO: Que pasa si el articulo no se encuentra? (el articulo no esta cargado)
                        Id = articleDB.Id,
                        OrderLineNo = OrderLineNo,
                        Name = articleDB?.Name,
                        Units = a.Units,
                        ModifList = a.ModifList
                    };
                    if (a.Menu == null)
                    {

                        if (articleDB.Monitors.Count > 0)
                        {
                            foreach (var monitor in articleDB.Monitors)
                            {
                                resArticle.MonitorList.Add((uint)monitor.Id);
                            }
                        }
                        else if (articleDB.Department.Monitors.Count > 0)
                        {
                            foreach (var monitor in articleDB.Department.Monitors)
                            {
                                resArticle.MonitorList.Add((uint)monitor.Id);
                            }
                        }
                        else
                        {
                            foreach (var monitor in articleDB.Department.Family.Monitors)
                            {
                                resArticle.MonitorList.Add((uint)monitor.Id);
                            }
                        }
                    }
                    else
                    {
                        resArticle.Menu = new()
                        {
                            Name = a.Menu.Name
                        };
                        foreach (var PrintOrder in a.Menu.printOrderList)
                        {
                            PrintOrderGroup? printOrderDb = _printOrderGroupService.GetById(PrintOrder.Id);
                            DTO.Responses.PrintOrder mPrintOrder = new()
                            {
                                Id = printOrderDb.Id,
                                Name = printOrderDb.Name
                            };
                            foreach (var menuItem in PrintOrder.ArticleList)
                            {
                                OrderLineNo++;
                                DAL.DAO.Article? articleDb = _articleService.GetById(menuItem.Id);
                                DTO.Responses.Article article = new()
                                {
                                    Id = articleDb.Id,
                                    OrderLineNo = OrderLineNo,
                                    Name = articleDb.Name,
                                    Units = menuItem.Units,
                                    ModifList = menuItem.ModifList
                                };
                                if (articleDb.Monitors.Count > 0)
                                {
                                    foreach (var monitor in articleDb.Monitors)
                                    {
                                        article.MonitorList.Add((uint)monitor.Id);
                                    }
                                }
                                else if (articleDb.Department.Monitors.Count > 0)
                                {
                                    foreach (var monitor in articleDb.Department.Monitors)
                                    {
                                        article.MonitorList.Add((uint)monitor.Id);
                                    }
                                }
                                else
                                {
                                    foreach (var monitor in articleDb.Department.Family.Monitors)
                                    {
                                        article.MonitorList.Add((uint)monitor.Id);
                                        resArticle.MonitorList.Add((uint)monitor.Id);
                                    }
                                }
                                mPrintOrder.ArticleList.Add(article);
                            }
                            resArticle.Menu.printOrderlist.Add(mPrintOrder);
                        }
                    }
                    print.ArticleList.Add(resArticle);
                }
                response.PrintOrderList.Add(print);

            }
            _orderService.save(response.ToDictionary());
            Console.WriteLine(response.ToString());
            
            await Clients.All.SendAsync("KitchenOrder", response);
        }

        public async Task ChangeStatus(ChangeStatus json)
        {
            // TODO: comprobar que el OldStatus coincide con el de BBDD y hacer el cambio
            _orderItemService.ChangeStatus(json.ToDictionary());
            if (_orderItemService.IsOrderFinished(json.OrderId, json.TermNo))
            {
                DTO.Responses.CloseTable closeTableDto = new()
                {
                    OrderId = json.OrderId,
                };
                await Clients.All.SendAsync("CloseTable", closeTableDto);
            }
            await Clients.All.SendAsync("ChangeStatus", json);
        }

        public async Task CloseTable(DTO.Requests.CloseTable json)
        {
            if (_orderItemService.IsOrderFinished(json.OrderId, json.TermNo))
            {
                await Clients.All.SendAsync("CloseTable", json);
            }
        }

    }
}
