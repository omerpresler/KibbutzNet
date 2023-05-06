using System.Runtime.ExceptionServices;
using Backend.Access;
using Backend.Business.src.Utils;
using Backend.Controllers.Requests;

namespace Backend.Business.src.Utils;
using System.Collections.Generic;

    public class OrderManager
    {
        // orders is a dictionary that the key is the storeID, and the value is a list of orders
        public Dictionary<int, List<Order>> orders;
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
                    }
                    return instance;
                }
            }
        }

        public Response<int> addOrder(Access.Order DALOrder)
        {
            Order order = new Order(DALOrder.orderID, DALOrder.memberName, DALOrder.memberId, DALOrder.active, DALOrder.cost, DALOrder.description);
            if (orders.ContainsKey(DALOrder.storeId))
                orders[DALOrder.storeId].Add(order);
            else
            {
                var newOrderList = new List<Order>();
                newOrderList.Add(order);
                orders.Add(DALOrder.storeId, newOrderList);
            }


            DBManager.Instance.AddOrder(order.ToDalObject(DALOrder.storeId));
            return new Response<int>(order.orderId);
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


            DBManager.Instance.AddOrder(order.ToDalObject(storeID));
            return new Response<int>(order.orderId);
        }

        public Response<string> changeOrdersStatus (int storeID, int orderID, string status)
        {
            foreach (var o in from order in orders where order.Key == storeID from o in order.Value where o.orderId == orderID select o)
            {
                o.status = status;
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
