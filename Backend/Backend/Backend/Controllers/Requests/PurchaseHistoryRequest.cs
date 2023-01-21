namespace Controllers.Requests;

public class PurchaseHistoryRequest
{
    public int StoreId { get; set; }
        
    public PurchaseHistoryRequest(int StoreId)
    {
        this.StoreId = StoreId;
    }
}