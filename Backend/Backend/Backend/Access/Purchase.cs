using System;

namespace Backend.Access;

public class Purchase
{
    public int memberId { get; set; }
    public int storeId { get; set; }
    public int purchaseId{ get; set; }
    public float cost { get; set; }
    public string description { get; set; }
    public DateTime date { get; set; }
    
    public Purchase(int storeId, int memberId, int purchaseId, float cost, string description, DateTime date)
    {
        this.storeId = storeId;
        this.memberId = memberId;
        this.purchaseId = purchaseId;
        this.cost = cost;
        this.description = description;
        this.date = date;
    }
}
