using System.Collections;
using Backend.Business.src.Client_Store;
using Backend.Business.src.Utils;


namespace Backend.Service;

public class Store
{
    private static Dictionary<int, ClientStoreService> stores;
    
    
    private static Store instance;
    private static readonly object padlock = new object();

    private Store()
    {
        stores = new Dictionary<int, ClientStoreService>();
    }

    public static Store Instance {
        get {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Store();
                }
                return instance;
            }
        }
    }

    public bool storeExist(int storeId)
    {
        return stores.ContainsKey(storeId);
    }
    
    public string getStoreName(int storeId)
    {
        return stores[storeId].storeName;
    }
    
    public void addNewStore(ClientStoreService store)
    {
        stores.Add(store.storeId, store);
    }
    
    public void LoadStores()
    {
        foreach (Access.Store DALStore in Access.DBManager.Instance.LoadStores())
        {
            stores.Add(DALStore.storeId, new ClientStoreService(DALStore));
            OrderManager.Instance.orders.Add(DALStore.storeId, new List<Order>());
        }
    }
    
    public void LoadOrders()
    {
        foreach (Access.Order DALOrders in Access.DBManager.Instance.LoadOrders())
        {
            OrderManager.Instance.addOrder(DALOrders);
        }
    }

    public Response<bool> Login(int userId, int storeId, string email)
    {
        try
        {
            AuthenticationManager.Instance.Login(userId, email);
            if(!AuthenticationManager.Instance.CheckWorkingPrivilege(storeId, userId))
                return new Response<bool>(true, $"User {userId} does not work at {storeId}");
        }
        catch (Exception e)
        {
            return new Response<bool>(true, e.Message);
        }


        return new Response<bool>(true);
    }

    public Response<string> SendMessage(int storeId, int userId, string msg)
    {
        try
        {
            if (stores.ContainsKey(storeId))
            {
                return stores[storeId].SendMessage(userId, msg);
            }

            return new Response<string>(true, $"The is no store with the id of {storeId}");
        }
        catch (Exception e)
        {
            return new Response<string>(true, e.Message);
        }
    }
    
    public Response<List<String>> GetAllchats(int storeId)
    {
        try
        {
            if (stores.ContainsKey(storeId))
            {
                return stores[storeId].GetAllchats();
            }

            return new  Response<List<String>>(true, $"The is no store with the id of {storeId}");
        }
        catch (Exception e)
        {
            return new  Response<List<String>>(true, e.Message);
        }
    }
    
    
    public Response<int> addOrder(int storeId, int memberId, string memberName, string description, float cost)
    {
        try
        {
            if (stores.ContainsKey(storeId))
            {
                return stores[storeId].addOrder(memberId, memberName, description, cost);
            }

            return new Response<int>(true, $"The is no store with the id of {storeId}");
        }
        catch (Exception e)
        {
            return new Response<int>(true, e.Message);
        }
    }
    
    public Response<string> changeOrdersStatus(int storeId, int orderId, string status)
    {
        try
        {
            if (stores.ContainsKey(storeId))
            {
                return stores[storeId].changeOrdersStatus(orderId, status);
            }

            return new Response<string>(true, $"The is no store with the id of {storeId}");
        }
        catch (Exception e)
        {
            return new Response<string>(true, e.Message);
        }
    }
    
    public Response<bool> closeOrder(int storeId, int orderId)
    {
        try
        {
            if (stores.ContainsKey(storeId))
            {
                return stores[storeId].closeOrder(storeId, orderId);
            }

            return new Response<bool>(true, $"The is no store with the id of {storeId}");
        }
        catch (Exception e)
        {
            return new Response<bool>(true, e.Message);
        }
    }
    
    public Response<bool> reOpenOrder(int storeId, int orderId)
    {
        try
        {
            if (stores.ContainsKey(storeId))
            {
                return stores[storeId].reOpenOrder(storeId, orderId);
            }

            return new Response<bool>(true, $"The is no store with the id of {storeId}");
        }
        catch (Exception e)
        {
            return new Response<bool>(true, e.Message);
        }
    }
    
    public Response<int> addPurchase(int storeId, int memberId, string description, float cost)
    {
        try
        {
            if (stores.ContainsKey(storeId))
            {
                return stores[storeId].addPurchase(memberId, description, cost);
            }

            return new Response<int>(true, $"The is no store with the id of {storeId}");
        }
        catch (Exception e)
        {
            return new Response<int>(true, e.Message);
        }
    }
    
    
    public Response<ArrayList> SeeOrderHistoryUser(int userId)
    {
        try
        {
            ArrayList jsons = new ArrayList();
            foreach(ClientStoreService store in stores.Values)
            {
                foreach (string purchase in store.GetOrderByUser(userId))
                {
                    jsons.Add(purchase);
                }
            }

            return new Response<ArrayList>(jsons);
        }
        catch (Exception e)
        {
            return new Response<ArrayList>(true, e.Message);
        }
        
    }

    public Response<ArrayList> SeeOrderHistoryStore(int storeId)
    {
        try
        {
            if (stores.ContainsKey(storeId))
                return new Response<ArrayList>(stores[storeId].printOrders());

            return new Response<ArrayList>(true, $"Store: {storeId} does not exist");

        }
        catch (Exception e)
        {
            return new Response<ArrayList>(true, e.Message);
        }
    }

    public Response<ArrayList> SeeOrderHistoryUserAndStore(int storeId, int userId)
    {
        try
        {
            if (stores.ContainsKey(storeId))
                return new Response<ArrayList>(stores[storeId].GetOrderByUser(userId));

            return new Response<ArrayList>(true, $"Store: {storeId} does not exist");

        }
        catch (Exception e)
        {
            return new Response<ArrayList>(true, e.Message);
        }
    }
    
    public Response<ArrayList> SeePurchaseHistoryUser(int userId)
    {
        try
        {
            ArrayList jsons = new ArrayList();
            foreach(ClientStoreService store in stores.Values)
            {
                foreach (string purchase in store.GetPurchasesByUser(userId))
                {
                    jsons.Add(purchase);
                }
            }

            return new Response<ArrayList>(jsons);
        }
        catch (Exception e)
        {
            return new Response<ArrayList>(true, e.Message);
        }
        
    }

    public Response<ArrayList> SeePurchaseHistoryStore(int storeId)
    {
        try
        {
            if (stores.ContainsKey(storeId))
                return new Response<ArrayList>(stores[storeId].printPurchases());

            return new Response<ArrayList>(true, $"Store: {storeId} does not exist");

        }
        catch (Exception e)
        {
            return new Response<ArrayList>(true, e.Message);
        }
    }

    public Response<ArrayList> SeePurchaseHistoryUserAndStore(int storeId, int userId)
    {
        try
        {
            if (stores.ContainsKey(storeId))
                return new Response<ArrayList>(stores[storeId].GetPurchasesByUser(userId));

            return new Response<ArrayList>(true, $"Store: {storeId} does not exist");

        }
        catch (Exception e)
        {
            return new Response<ArrayList>(true, e.Message);
        }
    }
    
    public Response<Post> AddPost(int storeId, String header, string photoLink)
    {
        try
        {
            if (stores.ContainsKey(storeId))
            {
                return stores[storeId].AddPost(header, photoLink);
            }

            return new Response<Post>(true, $"The is no store with the id of {storeId}");
        }
        catch (Exception e)
        {
            return new Response<Post>(true, e.Message);
        }
    }

    public Response<Post> RemovePost(int storeId, int postId)
    {
        try
        {
            if (stores.ContainsKey(storeId))
            {
                return stores[storeId].RemovePost(postId);
            }

            return new Response<Post>(true, $"The is no store with the id of {storeId}");
        }
        catch (Exception e)
        {
            return new Response<Post>(true, e.Message);
        }
    }

    public Response<Product> AddProduct(int storeId, string name, string description)
    {
        try
        {
            if (stores.ContainsKey(storeId))
            {
                return stores[storeId].AddProduct(name, description);
            }

            return new Response<Product>(true, $"The is no store with the id of {storeId}");
        }
        catch (Exception e)
        {
            return new Response<Product>(true, e.Message);
        }
    }
        
    public Response<Product> RemoveProduct(int storeId, int productId)
    {
        try
        {
            if (stores.ContainsKey(storeId))
            {
                return stores[storeId].RemoveProduct(productId);
            }

            return new Response<Product>(true, $"The is no store with the id of {storeId}");
        }
        catch (Exception e)
        {
            return new Response<Product>(true, e.Message);
        }
    }

    public Response<List<Tuple<int, String?, String?>>> getAllStores()
    {
        List<Tuple<int, String?, String?>> allStores = new List<Tuple<int, string?, string?>>();
        
        foreach (int key in stores.Keys)
            allStores.Add(new Tuple<int, String?, String?>(key, stores[key].storeName, stores[key].photoLink));

        return new Response<List<Tuple<int, String?, String?>>>(allStores);
    }

    public Response<string> sendEmailReport(int storeId, string email)
    {
        try
        {
            if (stores.ContainsKey(storeId))
                return stores[storeId].GenerateReport(email);
            

            return new Response<string>(true, $"The is no store with the id of {storeId}");
        }
        catch (Exception e)
        {
            return new Response<string>(true, e.Message);
        }
    }
    
    public Response<List<Post>> getPosts(int storeId)
    {
        return stores[storeId].getPosts();
    }
}