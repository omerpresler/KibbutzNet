namespace Backend.Access;

public class StoreEmployee
{
    public int UserId { get; set; }
    public int storeId { get; set; }
   
    
    public StoreEmployee(int userId, int storeId)
    {
        this.storeId = storeId;
        UserId = userId;
    }

}