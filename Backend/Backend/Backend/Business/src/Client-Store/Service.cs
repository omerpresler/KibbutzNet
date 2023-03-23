using Backend.Business.src.Reports;
using Backend.Business.src.Utils;
using Backend.Business.Utils;

namespace Backend.Business.src.Client_Store;

public class Service
{
    private ChatManager _chatManager;
    private OrderManager _orderManager;
    private OutputManager _outputManager;
    private WorkerManager _workerManager;
    private User employee;
    private NotificationManager _notificationManager;
    private List<PageManager> stores;
    private int storeNums = 0;


    public Service()
    {
        stores = new List<PageManager>();
    }

    public void addStore(string storeName)
    {
        var store = new PageManager(storeName, Interlocked.Increment(ref storeNums));
        stores.Add(store);
        Console.WriteLine($"The store {storeName} was added.");
    }

    public void removeStore(string storeName)
    {
        foreach (var store in stores.Where(store => storeName == store.getName()))
        {
            stores.Remove(store);
            Console.WriteLine($"The store '{storeName} was deleted.");
            return;
        }
        Console.WriteLine($"There was no such store: '{storeName}'");
    }

    public List<Order> createReport(int storeID)
    {
        // This function should return a list of strings and it returns a list of orders
        return _orderManager.ordersByStoreID(storeID);
    }
    
    
}