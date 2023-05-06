using System.Collections;
using Backend.Business.src.Utils;
using Backend.Business.src.Reports;
using Backend.Business.Utils;
using Newtonsoft.Json;

namespace Backend.Business.src.Client_Store;

public class ClientStoreService
{
    private int storeId;
    private ChatManager chatManager;
    private OutputManager outputManager;
    private WorkerManager workerManager;
    private User employee;
    private NotificationManager notificationManager;
    private PageManager pageManager;
    private List<Purchase> purchases;
    private String storeName;
    
    public ClientStoreService(int storeId, int userId, string email)
    {
        this.storeId = storeId;
        purchases = new List<Purchase>();
        chatManager = new ChatManager();
        outputManager = new OutputManager();
        workerManager = new WorkerManager();
        notificationManager = new NotificationManager();
        employee = AuthenticationManager.Instance.Login(userId, email);

        storeName = AuthenticationManager.Instance.CheckWorkingPrivilege(storeId, userId);
        if (storeName == null)
            throw new Exception($"User {userId} does not work at {storeId}");
        
        pageManager = new PageManager(storeName);
    }
    
    public ClientStoreService(Access.Store DALStore)
    {
        storeId = DALStore.storeId;
        storeName = DALStore.storeName;
        
        purchases = new List<Purchase>();
        chatManager = new ChatManager();
        outputManager = new OutputManager();
        workerManager = new WorkerManager();
        notificationManager = new NotificationManager();

        pageManager = new PageManager(storeName);
    }

    public Response<Product> AddProduct(string name, string description)
    {
        Product prod = new Product(name, description);
        return pageManager.AddProduct(prod);
    }

    public Response<int> OpenChat(int userId)
    {
        return chatManager.StartChat(storeId, userId);
    }

    public Response<string> SendMessage(int sessionId, string msg)
    {
        return chatManager.SendMessage(sessionId, new Message<string>(storeId, msg));
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
            {
                var purchase = new
                {
                    orderID = order.orderId,
                    date = order.date,
                    status = order.status,
                    memberName = order.memberName,
                    cost = order.cost,
                    description = order.description
                };
                jsons.Add(JsonConvert.SerializeObject(purchase));
            }
        return jsons;
    }
    
    public ArrayList printOrders()
    {
        
        ArrayList jsons = new ArrayList();
        foreach (Order order in OrderManager.Instance.orders[storeId])
        {
            var purchase = new
            {
                orderID = order.orderId,
                date = order.date,
                status = order.status,
                memberName = order.memberName,
                cost = order.cost,
                description = order.description
            };
            jsons.Add(JsonConvert.SerializeObject(purchase));
        }
        
        return jsons;
    }
    
    public ArrayList GetPurchasesByUser(int userId)
    {
        ArrayList jsons = new ArrayList();
        foreach (Purchase p in purchases)
            if (p.memberId == userId)
            {
                var purchase = new
                {
                    PurchaseID = p.purchaseId,
                    Date = p.date,
                    memberId = p.memberId,
                    Cost = p.cost,
                    Description = p.description
                };
                jsons.Add(JsonConvert.SerializeObject(purchase));
            }

        return jsons;
    }

    public ArrayList printPurchases()
    {
        ArrayList jsons = new ArrayList();
        foreach (Purchase p in purchases)
        {
            var purchase = new
            {
                PurchaseID = p.purchaseId,
                Date = p.date,
                memberId = p.memberId,
                Cost = p.cost,
                Description = p.description
            };
            jsons.Add(JsonConvert.SerializeObject(purchase));
        }

        return jsons;
    }

}