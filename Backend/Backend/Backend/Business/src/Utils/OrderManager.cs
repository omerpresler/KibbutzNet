using System.Runtime.ExceptionServices;
using System.Text.Json;
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
            try
            {
                orderNum = DBManager.Instance.getMaxOrderId()+1;
            }
            catch (Exception e)
            {
                orderNum = 0;
            }
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

        public Response<string> changeOrdersStatus (int storeId, int orderId, string status)
        {
            Order? order = orders[storeId].Find(x => x.orderId == orderId);
            
            if (order == null)
            {
                return new Response<string>(true, "No such order");
            }
            DBManager.Instance.updateOrderStatusField(order.orderId, status);
            order.status = status;

            return new Response<string>(status);
        }
        
        public Response<bool> closeOrder(int storeId, int orderId)
        {
            Order order = orders[storeId].Find(x => x.orderId == orderId);
            Console.WriteLine("Found order: " + (order != null ? order.orderId.ToString() : "null"));
            
            if (order == null)
            {
                return new Response<bool>(true, "No such order");
            }
            DBManager.Instance.updateOrderActiveField(order.orderId, false);
            order.active = false;

            return new Response<bool>(true);
        }
    
        public Response<bool> reOpenOrder(int storeId, int orderId)
        {
            Order? order = orders[storeId].Find(x => x.orderId == orderId);
            
            if (order == null)
            {
                return new Response<bool>(true, "No such order");
            }
            DBManager.Instance.updateOrderActiveField(order.orderId, true);
            order.active = true;

            return new Response<bool>(true);
        }
        
        public List<object> GenerateOrderReport(int storeId)
        {
            List<object> orderData = new List<object>();

            foreach (Order order in orders[storeId])
            {
                var orderObject = new
                {
                    order.orderId,
                    order.date,
                    order.status,
                    order.memberName,
                    order.memberId,
                    order.active,
                    order.cost,
                    order.description
                };

                orderData.Add(orderObject);
            }

            return orderData;
        }

    }
