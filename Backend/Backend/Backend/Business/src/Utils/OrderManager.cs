using System.Runtime.ExceptionServices;
using Backend.Business.src.Utils;
using Backend.Controllers.Requests;

namespace Backend.Business.src.Utils;
using System.Collections.Generic;

    public class OrderManager
    {
        // orders is a dictionary that the key is the storeID, and the value is a list of orders
        public static Dictionary<int, List<Order>> orders;
        private static int orderNum = 0;
        private static OrderManager instance;
        private static readonly object padlock = new object();

        public OrderManager()
        {
            orders = new Dictionary<int, List<Order>>();
        }
        
        public static OrderManager Instance {
            get {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new OrderManager();
                        
                        //init
                        orders.Add(0, new List<Order>());
                        orders[0].Add(new Order(Interlocked.Increment(ref orderNum), "Amit", 0, false, (float)1105.7, "init purchase"));
                    }
                    return instance;
                }
            }
        }

        public Response<int> addOrder(int storeID, int memberID, string memberName, string description, float cost)
        {
            Order order = new Order(Interlocked.Increment(ref orderNum), memberName, memberID, true, cost, description);
            if (orders.ContainsKey(storeID))
                orders[storeID].Add(order);
            else
            {
                var newOrderList = new List<Order>();
                newOrderList.Add(order);
                orders.Add(storeID, newOrderList);
            }
            return new Response<int>(order.getID());
        }

        public Response<string> changeOrdersStatus (int storeID, int orderID, string status)
        {
            foreach (var o in from order in orders where order.Key == storeID from o in order.Value where o.getID() == orderID select o)
            {
                o.setStatus(status);
            }

            return new Response<string>(status);
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
