using Backend.Access;
using Backend.Business.src.Client_Store;
using Backend.Business.src.Utils;

namespace Backend.Business.src;

public class Admin
{
    public int UserId { get; set; }
    public string email { get; set; }
    private static int _nextStoreId;

    private static int AssignStoreId()
    {
        return Interlocked.Increment(ref _nextStoreId);
    }

    public Admin(Access.Admin admin)
    {
        this.UserId = admin.UserId;
        this.email = admin.email;
        try
        {
            _nextStoreId = DBManager.Instance.getMaxStoreId();
        }
        catch (Exception)
        {
            _nextStoreId = 0;
        }
        
    }


    public void ConnectEmployeeToStore(int userId, int storeId)
    {
        DBManager.Instance.AddStoreEmployee(new StoreEmployee(userId, storeId));
        AuthenticationManager.Instance.AddStoreEmployee(userId, storeId);
    }

    public MemberController.MemberController CreateNewMember(int userId, string name,string phoneNumber, string email)
    {
        DBManager.Instance.AddMember(userId, name, phoneNumber, email);
        return new MemberController.MemberController(userId, name, phoneNumber, email);
    }
    
    public ClientStoreService CreateStore(string storeName, string photoLink)
    {
        int storeId = AssignStoreId();
        DBManager.Instance.AddStore(storeId, storeName, photoLink);
        return new ClientStoreService(storeId, storeName, photoLink);
    }
}