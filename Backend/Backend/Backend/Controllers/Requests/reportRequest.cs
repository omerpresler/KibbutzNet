namespace Backend.Controllers.Requests;

public class reportRequest
{
    public int storeId {get; set;}
    public string email {get; set;}
    
    public reportRequest(int storeId, string email)
    {
        this.storeId = storeId;
        this.email = email;
    }
}