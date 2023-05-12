using System.Collections;
using Backend.Access;
using Backend.Business.src.Client_Store;
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

    public Response<string> Login(int userId, string email)
    {
        try
        {
            if (!admins.ContainsKey(userId))
                return new Response<string>(true, $"No admin with the id of {userId}");
            
            if(!admins[userId].email.Equals(email))
                return new Response<string>(true, $"Email does not match the id");

            return new Response<string>("admin");
        }
        catch (Exception e)
        {
            return new Response<string>(true, e.Message);
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
    public Response<bool> CreateNewStore(int adminId, string storeName)
    {
        try
        {
            if (!admins.ContainsKey(adminId))
                return new Response<bool>(true, $"Admin {adminId} does not exist");

            Store.Instance.addNewStore(admins[adminId].CreateStore(storeName));
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
            
            User.Instance.addNewMember(admins[adminId].CreateNewMember(userId, name,phoneNumber, email));
        }
        catch (Exception e)
        {
            return new Response<bool>(true, e.Message);
        }
        
        return new Response<bool>(true);
    }
}