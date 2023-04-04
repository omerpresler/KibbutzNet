using System.Collections;
using Backend.Business.src.Utils;

namespace Backend.Service;
using Backend.Business.src.StoreRegister;

public class Register
{
    private static Dictionary<int, StoreRegister> registers;
    
    private static Register instance;
    private static readonly object padlock = new object();

    private Register()
    {
        registers = new Dictionary<int, StoreRegister>();
        this.OpenRegister(0,0);
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


    public Response<string> OpenRegister(int StoreId, int EmployeeId)
    {
        try
        {
            StoreRegister register;
            if (!registers.ContainsKey(StoreId))
            {
                register = new StoreRegister(StoreId);
                registers.Add(StoreId, register);
            }
            else
            {
                register = registers[StoreId];
            }
            
            
            
        } catch (Exception e)
        {
            return new Response<bool>(true, e.Message);
        }
    }

    public Response<bool> CloseRegister(int StoreId)
    {
        try
        {
            if (registers.ContainsKey(StoreId))
            {
                registers[StoreId].logout();
                registers.Remove(StoreId);
            }
            else
            {
                return new Response<bool>(true, $"Store: {StoreId} does not exist");
            }

            return new Response<bool>(true);
        } catch (Exception e)
        {
            return new Response<bool>(true, e.Message);
        }
    }
    
    public Response<int> addPurchase(int StoreId, int BudgetNumber, string Description, float Cost)
    {
        return registers.ContainsKey(StoreId) ? registers[StoreId].addPurchase(BudgetNumber, Description, Cost) : new Response<int>(true, "The register has not been opened");
    }
    
    public Response<List<string>> SeePurchaseHistoryUser(int userId)
    {
        foreach(IRegisterService register in registers.Values)
        {
            
        }
    }
    //register -add new purchse see purchse history
    //store-client-get report
    
    public ArrayList SeePurchaseHistoryStore(int StoreId, DateTime start)
    {
    }
    
    public ArrayList SeePurchaseHistoryUserAndStore(int StoreId, DateTime start, DateTime end)
    {
    }
}