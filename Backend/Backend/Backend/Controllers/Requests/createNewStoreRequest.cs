namespace Backend.Controllers.Requests;

public class createNewStoreRequest
{
    public int adminId {get; set;}
    public string storeName {get; set;}
    
    public createNewStoreRequest(int adminId, string storeName)
    {
        this.adminId = adminId;
        this.storeName = storeName;
    }
}