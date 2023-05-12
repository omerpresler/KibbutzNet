namespace Backend.Controllers.Requests;

public class changeOrdersActiveStateRequest
{
    public int storeId {get; set;}
    public int orderId { get; set; }
    
    public changeOrdersActiveStateRequest(int storeId, int orderId)
    {
        this.storeId = storeId;
        this.orderId = orderId;
    }
}