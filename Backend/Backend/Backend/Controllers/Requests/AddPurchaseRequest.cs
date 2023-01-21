namespace Backend.Controllers.Requests;

public class AddPurchaseRequest
{
    public float Cost { get; set; }
    public string Description { get; set; }
    public int BudgetNumber { get; set; }
    public int StoreId { get; set; }

    public AddPurchaseRequest(float cost, string description, int budgetNumber, int storeId)
    {
        Cost = cost;
        Description = description;
        BudgetNumber = budgetNumber;
        StoreId = storeId;
    }
}