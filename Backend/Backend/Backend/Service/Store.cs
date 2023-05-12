﻿using System.Collections;
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
    
    public void LoadStores()
    {
        foreach (Access.Store DALStore in Access.DBManager.Instance.LoadStores())
        {
            stores.Add(DALStore.storeId, new ClientStoreService(DALStore));
            AuthenticationManager.Instance.AddStore(DALStore.storeId, DALStore.storeName);
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
            if(AuthenticationManager.Instance.CheckWorkingPrivilege(storeId, userId) == null)
                return new Response<bool>(true, $"User {userId} does not work at {storeId}");
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
    

}