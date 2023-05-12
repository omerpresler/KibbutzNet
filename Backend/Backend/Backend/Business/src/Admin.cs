using Backend.Access;
using Backend.Business.src.Utils;

namespace Backend.Business.src;

public class Admin
{
    public int UserId { get; set; }
    public string email { get; set; }

    public Admin(Access.Admin admin)
    {
        this.UserId = admin.UserId;
        this.email = admin.email;
    }


    public void ConnectEmployeeToStore(int userId, int storeId)
    {
        DBManager.Instance.AddStoreEmployees(new StoreEmployee(userId, storeId));
        AuthenticationManager.Instance.AddStoreEmployee(userId, storeId);
    }

    public void CreateNewMember(int userId, string name,string phoneNumber, string email)
    {
        DBManager.Instance.AddMember(userId, name, phoneNumber, email);
    }
    
    public void CreateStore(int storeId, string storeName)
    {
        DBManager.Instance.AddStore(storeId, storeName);
    }
}