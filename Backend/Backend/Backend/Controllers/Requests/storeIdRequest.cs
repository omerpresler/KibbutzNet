namespace Backend.Controllers.Requests;

public class storeIdRequest
{

    public int storeId { get; set; }


    public storeIdRequest(int storeId)
    {
        this.storeId = storeId;
    }
}