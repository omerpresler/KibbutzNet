namespace Backend.Controllers.Requests;

public class newPurchaseRequest
{
    public int memberId { get; set; }
    public int storeId { get; set; }
    public float cost { get; set; }
    public string description { get; set; }
    
    public newPurchaseRequest(int memberId, int storeId, float cost, string description)
    {
        this.memberId = memberId;
        this.storeId = storeId;
        this.cost = cost;
        this.description = description;
    }
}