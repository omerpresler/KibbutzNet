using System.Collections;
using Backend.Access;
using Backend.Business.src.Utils;
using Backend.Business.src.Reports;
using Backend.Business.Utils;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Message = Backend.Business.src.Utils.Message;
using Order = Backend.Business.src.Utils.Order;
using Purchase = Backend.Business.src.Utils.Purchase;

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
    private static int nextPurchase = 0;

    
    public ClientStoreService(int storeId, string storeName, string photoLink)
    {
        this.storeId = storeId;
        this.storeName = storeName;
        this.photoLink = photoLink;

        
        purchases = new List<Purchase>();
        chatManager = new ChatManager();
        outputManager = new OutputManager();
        workerManager = new WorkerManager();
        notificationManager = new NotificationManager();
        pageManager = new PageManager(storeId);
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

        foreach (Access.Purchase p in DBManager.Instance.LoadPurchasesPerStore(storeId))
        {
            purchases.Add(new Purchase(p.memberId, storeId, p.purchaseId, p.cost, p.description, p.date));
        }
        
        try
        {
            nextPurchase = DBManager.Instance.getMaxPurchaseId();
        }
        catch (Exception)
        {
            nextPurchase = 0;
        }
    }

    public bool chatExist(int userId, int storeId)
    {
        return chatManager.chatExist(userId, storeId);
    }

    public Response<string> SendMessage(int userId, string msg)
    {
        return chatManager.SendMessage(userId, storeId, new Message(true, msg), true);
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
        Purchase p = new Purchase(memberID, storeId, Interlocked.Increment(ref nextPurchase),cost, description, DateTime.Now);
        purchases.Add(p);
        DBManager.Instance.AddPurchase(storeId, memberID, p.purchaseId, cost, description, p.date);
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
    
    public Response<Post> AddPost(String header, string photoLink)
    {
        return pageManager.AddPost(header, photoLink);
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
    
    public string GenerateReport()
    {
        List<object> purchaseData = new List<object>();

        foreach (Purchase purchase in purchases)
        {
            string simpleTime = purchase.date.ToString("yyyy-MM-dd HH:mm:ss");
            var purchaseObject = new
            {
                purchase.purchaseId,
                purchase.memberId,
                purchase.storeId,
                purchase.cost,
                purchase.description,
                Time = simpleTime
            };

            purchaseData.Add(purchaseObject);
        }

        List<object> orderData = OrderManager.Instance.GenerateOrderReport(storeId);
        
        var report = new
        {
            Purchases = purchaseData,
            Orders = orderData
        };

        
        return JsonSerializer.Serialize(report);
    }


    public Response<string> sendEmailReport(string email)
    {
        try
        {
            outputManager.sendEmail(email, "New Test", GenerateReport());
            return new Response<string>(email);
        }
        catch (Exception e)
        {
            return new Response<string>(true, e.Message);
        }
    }
    
    public Response<string> saveExcelReport()
    {
        try
        {
            string path = "../../test.csv";
            //outputManager.saveExcelFile(GenerateReport(), path);
            return new Response<string>(path);
        }
        catch (Exception e)
        {
            return new Response<string>(true, e.Message);
        }
    }


    public Response<List<Post>> getPosts()
    {
        return pageManager.getPosts();
    }

}