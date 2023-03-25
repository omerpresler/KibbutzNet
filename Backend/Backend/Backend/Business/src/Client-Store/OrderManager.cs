using Backend.Controllers.Requests;

namespace Backend.Business.src.Client_Store;
using System.Collections.Generic;

    public class OrderManager
    {
        // has all of the orders of all of the stores
        
        
        // orders is a dictionary that the key is the storeID, and the value is a list of orders
        private Dictionary<int, List<Order>> orders;
        private int orderNum = 0;

        public OrderManager()
        {
            orders = new Dictionary<int, List<Order>>();
        }

        public response<string> addOrder(int storeID, int memberID, string memberName, string description, float cost)
        {
            var order = new Order(Interlocked.Increment(ref orderNum), memberName, memberID, true, cost, description);
            if (orders.ContainsKey(storeID))
                orders[storeID].Add(order);
            else
            {
                var newOrderList = new List<Order>();
                newOrderList.Add(order);
                orders.Add(storeID, newOrderList);
            }

            var res = new response<string>("order was added.", false);
            return res;
        }

        public response<string> changeOrdersStatus (int storeID, int orderID, string status)
        {
            foreach (var o in from order in orders where order.Key == storeID from o in order.Value where o.getID() == orderID select o)
            {
                o.setStatus(status);
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
