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

            return register.login(EmployeeId);
            
            
        } catch (Exception e)
        {
            return new Response<string>(true, e.Message);
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
    
    /*
    
    public Response<int> addPurchase(int storeId, int budgetNumber, string description, float cost)
    {
        return registers.ContainsKey(storeId) ? registers[storeId].addPurchase(budgetNumber, description, cost) : new Response<int>(true, "The register has not been opened");
    }
    
    public Response<ArrayList> SeePurchaseHistoryUser(int userId)
    {
        try
        {
            ArrayList jsons = new ArrayList();
            foreach(StoreRegister register in registers.Values)
            {
                foreach (string purchase in register.GetPurchasesByUser(userId))
                {
                    jsons.Add(purchase);
                }
            }

            return new Response<ArrayList>(jsons);
        }
        catch (Exception e)
        {
            return new Response<ArrayList>(true, e.Message);
        }
        
    }
    //register -add new purchse see purchse history
    //store-client-get report
    
    public Response<ArrayList> SeePurchaseHistoryStore(int storeId)
    {
        try
        {
            if (registers.ContainsKey(storeId))
                return new Response<ArrayList>(registers[storeId].print());

            return new Response<ArrayList>(true, $"Store: {storeId} does not exist");

        }
        catch (Exception e)
        {
            return new Response<ArrayList>(true, e.Message);
        }
    }
    
    public Response<ArrayList> SeePurchaseHistoryUserAndStore(int storeId, int userId)
    {
        try
        {
            if (registers.ContainsKey(storeId))
                return new Response<ArrayList>(registers[storeId].GetPurchasesByUser(userId));

            return new Response<ArrayList>(true, $"Store: {storeId} does not exist");

        }
        catch (Exception e)
        {
            return new Response<ArrayList>(true, e.Message);
        }
        
    }
    */
}