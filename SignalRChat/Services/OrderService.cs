using DAL.Models;
using DAL.Repositories;
using DAL.Services;

namespace SignalRChat.Services
{
    public class OrderService
    {
        private readonly IGenericCRUD<PrintOrderGroup> _printOrderGroupRepo;
        private readonly IGenericCRUD<Article> _articleRepo;
        private readonly IGenericCRUD<OrderItem> _orderItemRepo;
        private readonly IGenericCRUD<Order> _orderRepo;
        private readonly IGenericCRUD<State> _stateRepo;

        public OrderService(IGenericCRUD<PrintOrderGroup> printOrderGroupRepo, IGenericCRUD<Article> articleRepo, IGenericCRUD<OrderItem> orderItemRepo, IGenericCRUD<Order> orderRepo, IGenericCRUD<State> stateRepo)
        {
            _printOrderGroupRepo = printOrderGroupRepo;
            _articleRepo = articleRepo;
            _orderItemRepo = orderItemRepo;
            _orderRepo = orderRepo;
            _stateRepo = stateRepo;
        }

        public DTO.Responses.KitchenOrder New(DTO.Requests.KitchenOrder order)
        {
            DTO.Responses.KitchenOrder kitchenOrder = new()
            {
                SoftwareVers = order.SoftwareVers,
                MadiCustNo = order.MadiCustNo,
                CompNo = order.CompNo,
                StoreNo = order.StoreNo,
                TermNo = order.TermNo,
                OperNo = order.OperNo,
                OperName = order.OperName,
                StartTime = DateTime.Now.ToString("u"),
                TbNum = order.TbNum,
                Pax = order.Pax,
                TableType = order.TableType,
            };
            Order dbOrder = new()
            {
                SoftwareVers = order.SoftwareVers,
                MadiCustNo = order.MadiCustNo,
                CompNo = order.CompNo,
                StoreNo = order.StoreNo,
                TermNo = order.TermNo,
                TransNo = order.TransNo,
                OperNo = order.OperNo,
                OperName = order.OperName,
                StartTime = DateTime.Now,
                TbNum = order.TbNum,
                Pax = order.Pax,
                TableType = order.TableType,
            };
            dbOrder = _orderRepo.Save(dbOrder);

            kitchenOrder.OrderId = dbOrder.Id;
            foreach (DTO.Requests.PrintOrder  printOrder in order.PrintOrderList)
            {
                int OrderLineNo = 0;
                PrintOrderGroup? orderGroupDb = _printOrderGroupRepo.GetById(printOrder.Id);
                if (orderGroupDb != null)
                {
                    DTO.Responses.PrintOrder po = new()
                    {
                        Id = orderGroupDb.Id,
                        Name = orderGroupDb.Name
                    };
                    foreach (DTO.Requests.Article article in printOrder.ArticleList)
                    {
                        OrderLineNo++;
                        Article? artDb = _articleRepo.GetById(article.Id);
                        if (artDb != null)
                        {
                            DTO.Responses.Article a = new()
                            {
                                Id = artDb.Id,
                                OrderLineNo = OrderLineNo,
                                Name = artDb.Name,
                                Units = article.Units,
                                ModifList = article.ModifList,
                                PeparationTime = artDb.PreparationTime,
                                StateId = _stateRepo.GetAll().OrderBy(state => state.Order).First().Id

                            };

                            OrderItem orderItem = new()
                            {
                                OrderId = dbOrder.Id,
                                OrderLineNo = OrderLineNo,
                                PrintOrderGroup = orderGroupDb,
                                Units = article.Units,
                                ModifList = String.Join(',', article.ModifList),
                                Article = artDb,
                                State = _stateRepo.GetAll().OrderBy(state => state.Order).First()


                            };
                            orderItem = _orderItemRepo.Save(orderItem);

                            if (article.Menu == null)
                            {
                                if (artDb.Monitors.Count > 0)
                                {
                                    foreach (DAL.Models.Monitor monitor in artDb.Monitors)
                                    {
                                        a.MonitorList.Add((uint) monitor.Id);
                                    }
                                }else if (artDb.Department.Monitors.Count > 0)
                                {
                                    foreach (DAL.Models.Monitor monitor in artDb.Department.Monitors)
                                    {
                                        a.MonitorList.Add((uint)monitor.Id);
                                    }
                                }
                                else
                                {
                                    foreach (DAL.Models.Monitor monitor in artDb.Department.Family.Monitors)
                                    {
                                        a.MonitorList.Add((uint)monitor.Id);
                                    }
                                }
                                
                            }
                            else
                            {
                                a.Menu = new DTO.Responses.Menu()
                                {
                                    Name = a.Menu.Name
                                };
                                
                                foreach (DTO.Requests.PrintOrder menuPrintOrder in article.Menu.printOrderList)
                                {
                                    PrintOrderGroup? menuOrderGroupDb = _printOrderGroupRepo.GetById(menuPrintOrder.Id);
                                    if (menuOrderGroupDb != null)
                                    {
                                        DTO.Responses.PrintOrder menuPO = new()
                                        {
                                            Id = menuOrderGroupDb.Id,
                                            Name = menuOrderGroupDb.Name
                                        };
                                        foreach (DTO.Requests.Article menuItem in menuPrintOrder.ArticleList)
                                        {
                                            OrderLineNo++;
                                            Article? menuItemDb = _articleRepo.GetById(menuItem.Id);
                                            if (menuItemDb != null)
                                            {
                                                DTO.Responses.Article menuItemRes = new()
                                                {
                                                    Id = menuItemDb.Id,
                                                    OrderLineNo = OrderLineNo,
                                                    Name = menuItemDb.Name,
                                                    Units = menuItem.Units,
                                                    ModifList = menuItem.ModifList,
                                                    StateId = _stateRepo.GetAll().OrderBy(state => state.Order).First().Id
                                                };
                                                OrderItem menuOrderItem = new()
                                                {
                                                    OrderId = dbOrder.Id,
                                                    OrderLineNo = OrderLineNo,
                                                    Units = menuItem.Units,
                                                    ModifList = String.Join(",", menuItem.ModifList),
                                                    MenuOrderLineNo = orderItem.OrderLineNo,
                                                    PrintOrderGroup = orderGroupDb,
                                                    Article = menuItemDb,
                                                    State = _stateRepo.GetAll().OrderBy(state => state.Order).First()
                                                };
                                                menuOrderItem = _orderItemRepo.Save(menuOrderItem);
                                                if (menuItemDb.Monitors.Count > 0)
                                                {
                                                    foreach (DAL.Models.Monitor monitor in menuItemDb.Monitors)
                                                    {
                                                        menuItemRes.MonitorList.Add((uint)monitor.Id);
                                                    }
                                                }
                                                else if(menuItemDb.Department.Monitors.Count > 0)
                                                {
                                                    foreach (DAL.Models.Monitor monitor in menuItemDb.Department.Monitors)
                                                    {
                                                        menuItemRes.MonitorList.Add((uint)monitor.Id);
                                                    }
                                                }
                                                else
                                                {
                                                    foreach (DAL.Models.Monitor monitor in menuItemDb.Department.Family.Monitors)
                                                    {
                                                        menuItemRes.MonitorList.Add((uint)monitor.Id);
                                                    }
                                                }
                                                menuPO.ArticleList.Add(menuItemRes);
                                            }
                                            else
                                            {

                                            }
                                            
                                            
                                        }
                                        a.Menu.printOrderlist.Add(menuPO);
                                    }
                                    else
                                    {

                                    }
                                }
                            }
                            
                            po.ArticleList.Add(a);
                        }
                        else
                        {

                        }
                    }
                    kitchenOrder.PrintOrderList.Add(po);
                }
                else
                {

                }
            }

            Console.WriteLine(_stateRepo.GetAll().OrderBy(s=> s.Order).First().Id);

            Console.WriteLine("despues de DB");
            Console.WriteLine(kitchenOrder.ToString());
            return kitchenOrder;
        }


        public bool IsOrderFinished(int orderId)
        {
            Order? orderDb = _orderRepo.GetById(orderId);
            if (orderDb != null)
            {
                State lastState = _stateRepo.GetAll()
                    .OrderBy(i => i.Order).First();
                
                List<OrderItem> itemList = orderDb.OrderItems.ToList();
                if (itemList.FindAll(i => i.State == lastState).Count == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
