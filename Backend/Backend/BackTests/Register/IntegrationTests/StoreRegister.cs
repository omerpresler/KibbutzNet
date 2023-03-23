using static Backend.Service.Register;

namespace BackTests.Register.IntegrationTests;

public class StoreRegister
{
    private Backend.Service.Register register;

    [SetUp]
    public void Setup()
    {
        register = Backend.Service.Register.Instance;
    }
    
    
    [Test]
    public void openStoreRegisterSuccess()
    {
        Assert.That(register.OpenRegister(99, 99), Is.True);
    }

    [Test]
    public void openStoreRegisterTwiceFail()
    {
        Assert.That(register.OpenRegister(99, 99), Is.True);
        Assert.That(register.OpenRegister(99, 99), Is.False);
    }
}