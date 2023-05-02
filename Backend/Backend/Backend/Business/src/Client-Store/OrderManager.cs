using System.Runtime.ExceptionServices;
using Backend.Business.src.Utils;
using Backend.Controllers.Requests;

namespace Backend.Business.src.Client_Store;
using System.Collections.Generic;

    public class OrderManager
    {
        // has all of the orders of all of the stores
        
        
        // orders is a dictionary that the key is the storeID, and the value is a list of orders
        private Dictionary<int, List<Order>> orders;
        private static int orderNum = 0;

        public OrderManager()
        {
            orders = new Dictionary<int, List<Order>>();
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
