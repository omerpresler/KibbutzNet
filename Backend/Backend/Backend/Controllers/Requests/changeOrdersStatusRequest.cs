namespace Backend.Controllers.Requests;

public class changeOrdersStatusRequest
{
    public int storeId { get; set; }
    public int orderId { get; set; }
    public string status { get; set; }

    public changeOrdersStatusRequest(int storeId, int orderId, string status)
    {
        this.storeId = storeId;
        this.orderId = orderId;
        this.status = status;
    }
}