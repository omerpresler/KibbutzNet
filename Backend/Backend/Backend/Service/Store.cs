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

    public Response<bool> Login(int userId, int storeId, string password)
    {
        

        try
        {
            if (stores.ContainsKey(storeId))
                return new Response<bool>(true, $"The is no store with the id of {storeId}");
            stores.Add(storeId, new ClientStoreService(storeId, userId, password));
        }
        catch (Exception e)
        {
            return new Response<bool>(true, e.Message);
        }


        return new Response<bool>(true);
    }
    
    public Response<int> OpenChat(int storeId, int userId)
    {
        try
        {
            if (stores.ContainsKey(storeId))
            {
                return stores[storeId].OpenChat(userId);
            }

            return new Response<int>(true, $"The is no store with the id of {storeId}");
        }
        catch (Exception e)
        {
            return new Response<int>(true, e.Message);
        }
    }

    public Response<string> SendMessage(int storeId, int sessionId, string msg)
    {
        try
        {
            if (stores.ContainsKey(storeId))
            {
                return stores[storeId].SendMessage(sessionId, msg);
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
                return stores[storeId].GetAllchats(storeId);
            }

            return new  Response<List<String>>(true, $"The is no store with the id of {storeId}");
        }
        catch (Exception e)
        {
            return new  Response<List<String>>(true, e.Message);
        }
    }
}