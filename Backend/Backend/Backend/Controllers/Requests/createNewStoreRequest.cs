namespace Backend.Controllers.Requests;

public class createNewStoreRequest
{
    public int adminId {get; set;}
    public int storeId {get; set;}
    public string storeName {get; set;}
    
    public createNewStoreRequest(int adminId, int storeId, string storeName)
    {
        this.adminId = adminId;
        this.storeId = storeId;
        this.storeName = storeName;
    }
}