using Backend.Business.src.StoreRegister;

namespace BackTests.Register.UnitTests;

public class Tests
{
    private StoreRegister regi;
    
    [SetUp]
    public void Setup()
    {
        regi = new StoreRegister(-1);
    }

    [Test]
    public void addPurchaseSuccess()
    {
        Assert.That(regi.addPurchase(1111, "cheese cake", 40, 1), Is.True);
        var p = regi.getPurchaseByBudgetNumber(1111);
        Assert.That(p.getDescription(), Is.EqualTo("cheese cake"));
    }

    [Test]
    public void addPurchaseWrongArg()
    {
        Assert.Multiple(() =>
        {
            Assert.That(regi.addPurchase(-1, "cheese cake", 40, 1), Is.False);
            Assert.That(regi.addPurchase(1111, "", 40, 1), Is.False);
            Assert.That(regi.addPurchase(1111, "cheese cake", -1, 1), Is.False);
            Assert.That(regi.addPurchase(1111, "cheese cake", 40, -1), Is.False);
        });
        
    }
}