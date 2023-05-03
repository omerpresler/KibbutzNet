namespace Backend.Controllers.Requests;

public class newOrderRequest
{
    public int storeId { get; set; }
    public int memberId { get; set; }
    public string memberName { get; set; }
    public string description { get; set; }
    public float cost { get; set; }

    public newOrderRequest(int storeId, int memberId, string memberName, string description, float cost)
    {
        this.storeId = storeId;
        this.memberId = memberId;
        this.memberName = memberName;
        this.description = description;
        this.cost = cost;
    }
}