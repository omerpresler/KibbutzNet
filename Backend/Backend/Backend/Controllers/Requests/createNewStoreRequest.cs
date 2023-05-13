namespace Backend.Controllers.Requests;

public class createNewStoreRequest
{
    public int adminId {get; set;}
    public string storeName {get; set;}
    public string photoLink {get; set;}
    
    public createNewStoreRequest(int adminId, string storeName, string photoLink)
    {
        this.adminId = adminId;
        this.storeName = storeName;
        this.photoLink = photoLink;
    }
}