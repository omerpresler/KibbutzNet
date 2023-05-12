using System.Collections;
using Backend.Access;
using Backend.Business.src.Utils;
using Backend.Business.src.StoreRegister;


namespace Backend.Service;

public class Admin
{
    private static Dictionary<int, Business.src.Admin> admins;
    
    private static Admin instance;
    private static readonly object padlock = new object();

    private Admin()
    {
        admins = new Dictionary<int, Business.src.Admin>();
        foreach (Access.Admin admin in DBManager.Instance.LoadAdmins())
        {
            admins.Add(admin.UserId, new Business.src.Admin(admin));
        }
    }

    public static Admin Instance {
        get {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Admin();
                }
                return instance;
            }
        }
    }

    public Response<bool> Login(int userId, string email)
    {
        try
        {
            if (!admins.ContainsKey(userId))
                return new Response<bool>(false);
            
            return new Response<bool>(admins[userId].email.Equals(email));
        }
        catch (Exception e)
        {
            return new Response<bool>(true, e.Message);
        }
    }

    public Response<bool> AssignEmployeeToStore(int adminId, int userId, int storeId)
    {
        try
        {
            if (!admins.ContainsKey(adminId))
                return new Response<bool>(true, $"Admin {adminId} does not exist");
            
            if (!Store.Instance.storeExist(storeId))
                return new Response<bool>(true, $"Store {storeId} does not exist");
            
            if (!User.Instance.userExist(userId))
                return new Response<bool>(true, $"User {userId} does not exist");
            
            admins[adminId].ConnectEmployeeToStore(userId, storeId);
        }
        catch (Exception e)
        {
            return new Response<bool>(true, e.Message);
        }
        
        return new Response<bool>(true);
    }
    
    // storeId is the budgetId of the original owner
    public Response<bool> CreateNewStore(int adminId, int storeId, string storeName)
    {
        try
        {
            if (!admins.ContainsKey(adminId))
                return new Response<bool>(true, $"Admin {adminId} does not exist");
            
            if (Store.Instance.storeExist(storeId))
                return new Response<bool>(true, $"Store {storeId} already exists");
            
            
            admins[adminId].CreateStore(storeId, storeName);
        }
        catch (Exception e)
        {
            return new Response<bool>(true, e.Message);
        }
        
        return new Response<bool>(true);
    }
    
    //budget number also serve as system id
    public Response<bool> CreateNewMember(int adminId, int userId, string name, string phoneNumber, string email)
    {
        try
        {
            if (!admins.ContainsKey(adminId))
                return new Response<bool>(true, $"Admin {adminId} does not exist");

            if (User.Instance.userExist(userId))
                return new Response<bool>(true, $"User {userId} already exists");
            
            admins[adminId].CreateNewMember(userId, name,phoneNumber, email);
        }
        catch (Exception e)
        {
            return new Response<bool>(true, e.Message);
        }
        
        return new Response<bool>(true);
    }
}