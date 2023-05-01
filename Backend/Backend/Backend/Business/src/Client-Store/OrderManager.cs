using Backend.Business.src.Utils;
using Backend.Controllers.Requests;
using NHibernate;

namespace Backend.Business.src.Client_Store;
using System.Collections.Generic;

    public class OrderManager
    {
        // has all of the orders of all of the stores
        
        
        // orders is a dictionary that the key is the storeID, and the value is a list of orders
        private Dictionary<int, List<Order>> orders;
        private int orderNum = 0;
        private readonly ISessionFactory _sessionFactory = NHibernateHelper.GetSessionFactory();

        public OrderManager()
        {
            orders = new Dictionary<int, List<Order>>();
        }

        public Response<string> addOrder(int storeID, int memberID, string memberName, string description, float cost)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var order = new Order(Interlocked.Increment(ref orderNum), memberName, memberID, true, cost,
                            description);
                        if (orders.ContainsKey(storeID))
                            orders[storeID].Add(order);
                        else
                        {
                            var newOrderList = new List<Order>();
                            newOrderList.Add(order);
                            orders.Add(storeID, newOrderList);
                        }

                        session.Save(order);
                        transaction.Commit();
                        return new Response<string>("Order was added to data-base.");
                    }
                    catch(Exception e)
                    {
                        transaction.Rollback();
                        return new Response<string>(true,"Can't add order to data-base.");
                    }
                }
            }
        }

        public response<string> changeOrdersStatus (int storeID, int orderID, string status)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                foreach (var o in from order in orders where order.Key == storeID from o in order.Value where o.getID() == orderID select o)
                {
                    var dataOrder = session.Get<Order>(orderID);
                    o.setStatus(status);
                    dataOrder.status = status;
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(dataOrder);
                        transaction.Commit();
                    }
                }
            }
            var res = new response<string>($"status changes to '{status}'.", false);
            return res;
        }

        public List<string>? ordersByStoreID(int storeID)
        {
            var ordersInString = new List<string>();
            foreach (var order in orders.Where(order => order.Key == storeID))
            {
                ordersInString.AddRange(order.Value.Select(o => o.ToString()));
                return ordersInString;
            }
            return null;
        }

    }
