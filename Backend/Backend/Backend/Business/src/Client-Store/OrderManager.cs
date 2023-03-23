namespace Backend.Business.src.Client_Store;
using System.Collections.Generic;

    public class OrderManager
    {
        // has all of the orders of all of the stores
        
        
        // orders is a dictionary that the key is the storeID, and the value is a list of orders
        private Dictionary<int, List<Order>> orders;


        public OrderManager()
        {
            orders = new Dictionary<int, List<Order>>();
        }

        public List<Order> ordersByStoreID(int storeID)
        {
            foreach (var order in orders)
            {
                if (order.Key == storeID)
                    return order.Value;
            }

            return null;
        }

    }
