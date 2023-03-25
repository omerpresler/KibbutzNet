using Backend.Business.src.Reports;
using Backend.Business.src.Utils;
using Backend.Business.Utils;
using Backend.Controllers.Requests;

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

    public response<string> removeStore(string storeName)
    {
        foreach (var store in stores.Where(store => storeName == store.getName()))
        {
            stores.Remove(store);
            return new response<string>($"The store '{storeName} was deleted.", false);
        }
        return new response<string>($"There was no such store: '{storeName}'", true);
    }

    public List<string> createReport(int storeID)
    {
        return _orderManager.ordersByStoreID(storeID);
    }
    
    
}