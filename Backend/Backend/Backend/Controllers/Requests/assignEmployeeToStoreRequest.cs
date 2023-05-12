namespace Backend.Controllers.Requests;

public class assignEmployeeToStoreRequest
{
    public int adminId {get; set;}
    public int userId {get; set;}
    public int storeId {get; set;}
    
    public assignEmployeeToStoreRequest(int adminId, int userId, int storeId)
    {
        this.adminId = adminId;
        this.userId = userId;
        this.storeId = storeId;
    }
}