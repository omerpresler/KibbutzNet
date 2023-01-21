namespace Backend.Service;
using Backend.Business.src.StoreRegister;

public class Register
{
    private static Dictionary<int, IRegisterService> registers;
    
    private static Register instance;
    private static readonly object padlock = new object();

    private Register()
    {
        registers = new Dictionary<int, IRegisterService>();
    }

    public static Register Instance {
        get {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Register();
                }
                return instance;
            }
        }
    }


    public bool OpenRegister(int StoreId, int EmployeeId)
    {
        if (!registers.ContainsKey(StoreId))
        {
            IRegisterService register = new RegisterService();
            register.login(StoreId, EmployeeId);
            registers.Add(StoreId, register);
        }
        else
        {
            IRegisterService register = registers[StoreId];
            register.changeEmployee(EmployeeId);
        }

        Console.WriteLine(registers.Count);
        return true;
    }
        
    public bool addPurchase(int StoreId, int BudgetNumber, string Description, float Cost)
    {
        Console.WriteLine(registers.Count);
        IRegisterService register = registers[StoreId];
        register.addPurchase(BudgetNumber, Description, Cost);
            
        return true;
    }
    
    public string SeePurchaseHistory(int StoreId)
    {
        IRegisterService register = registers[StoreId];
            
        return register.printPurchases();;
    }
    //register -add new purchse see purchse history
    //store-client-get report 
}