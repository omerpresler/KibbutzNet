using System.Collections;
using Backend.Business.src.Utils;
using Backend.Business.src.Reports;
using Backend.Business.Utils;
using Newtonsoft.Json;

namespace Backend.Business.src.Client_Store;

public class ClientStoreService
{
    public int storeId;
    private ChatManager chatManager;
    private OutputManager outputManager;
    private WorkerManager workerManager;
    private NotificationManager notificationManager;
    private PageManager pageManager;
    private List<Purchase> purchases;
    public String? storeName;
    public String? photoLink;
    
    public ClientStoreService(int storeId, string photoLink)
    {
        this.storeId = storeId;
        purchases = new List<Purchase>();
        chatManager = new ChatManager();
        outputManager = new OutputManager();
        workerManager = new WorkerManager();
        notificationManager = new NotificationManager();
        pageManager = new PageManager(storeId);
        this.photoLink = photoLink;
    }
    
    public ClientStoreService(Access.Store DALStore)
    {
        storeId = DALStore.storeId;
        storeName = DALStore.storeName;
        photoLink = DALStore.photoLink;
        
        purchases = new List<Purchase>();
        chatManager = new ChatManager();
        outputManager = new OutputManager();
        workerManager = new WorkerManager();
        notificationManager = new NotificationManager();

        pageManager = new PageManager(storeId);
        
        
    }
    
    public Response<int> OpenChat(int userId)
    {
        return chatManager.StartChat(storeId, userId);
    }

    public Response<string> SendMessage(int sessionId, string msg)
    {
        return chatManager.SendMessage(sessionId, new Message(true, msg), true);
    }

    public Response<List<String>> GetAllchats()
    {
        return chatManager.GetAllStoreChats(storeId);
    }

    public Response<int> addOrder(int memberID, string memberName, string description, float cost)
    {
        return OrderManager.Instance.addOrder(storeId, memberID, memberName, description, cost);
    }

    public Response<string> changeOrdersStatus (int orderID, string status)
    {
        return OrderManager.Instance.changeOrdersStatus(storeId, orderID, status);
    }
    
    public Response<bool> closeOrder(int storeId, int orderId)
    {
        return OrderManager.Instance.closeOrder(storeId, orderId);
    }
    
    public Response<bool> reOpenOrder(int storeId, int orderId)
    {
        return OrderManager.Instance.reOpenOrder(storeId, orderId);
    }
    
    public Response<int> addPurchase(int memberID, string description, float cost)
    {
        Purchase p = new Purchase(storeId, memberID, cost, description);
        purchases.Add(p);
        return new Response<int>(p.purchaseId);
    }
    
    
    public ArrayList GetOrderByUser(int userId)
    {
        ArrayList jsons = new ArrayList();
        foreach (Order order in OrderManager.Instance.orders[storeId])
            if (order.memberId == userId)
                jsons.Add(order);
            
        return jsons;
    }
    
    public ArrayList printOrders()
    {
        
        ArrayList jsons = new ArrayList();
        foreach (Order order in OrderManager.Instance.orders[storeId])
        {
            jsons.Add(JsonConvert.SerializeObject(order));
        }
        
        return jsons;
    }
    
    public ArrayList GetPurchasesByUser(int userId)
    {
        ArrayList jsons = new ArrayList();
        foreach (Purchase p in purchases)
            if (p.memberId == userId)
                jsons.Add(JsonConvert.SerializeObject(p));
            

        return jsons;
    }

    public ArrayList printPurchases()
    {
        ArrayList jsons = new ArrayList();
        foreach (Purchase p in purchases)
            jsons.Add(JsonConvert.SerializeObject(p));
        

        return jsons;
    }
    
    public Response<Post> AddPost(String header)
    {
        return pageManager.AddPost(header);
    }

    public Response<Post> RemovePost(int postId)
    {
        return pageManager.RemovePost(postId);
    }

    public Response<Product> AddProduct(string name, string description)
    {
        return pageManager.AddProduct(new Product(name, description));
    }
        
    public Response<Product> RemoveProduct(int productId)
    {
        return pageManager.RemoveProduct(productId);
    }

}