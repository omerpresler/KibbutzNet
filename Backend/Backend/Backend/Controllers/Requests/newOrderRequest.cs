namespace Backend.Controllers.Requests;

public class newOrderRequest
{
    public int storeId;
    public int memberId;
    public string memberName;
    public string description;
    public float cost;

    public newOrderRequest(int storeId, int memberId, string memberName, string description, float cost)
    {
        this.storeId = storeId;
        this.memberId = memberId;
        this.memberName = memberName;
        this.description = description;
        this.cost = cost;
    }
}