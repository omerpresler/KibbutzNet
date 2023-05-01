using Backend.Business.src.Client_Store;
using Backend.Business.Utils;
using NUnit.Framework;
namespace Backend.Business.Test.UnitTests;

public class OrderManagarTest
{
    [Test]
    public void addOrder()
    {
        var orderManager = new OrderManager();
        var member = new Member(1, 1, "client", "051-111-1111");
        var store = new PageManager(1, "store");
        orderManager.addOrder(1, 1, "rotem", "new order", 30);
        
    }
        
    
    
}