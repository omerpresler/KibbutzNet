namespace Backend.Controllers.Requests;

public class changeOrdersStatusRequest
{
    public int storeId;
    public int orderId;
    public string status;

    public changeOrdersStatusRequest(int storeId, int orderId, string status)
    {
        this.storeId = storeId;
        this.orderId = orderId;
        this.status = status;
    }
}