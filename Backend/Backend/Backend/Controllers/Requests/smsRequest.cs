namespace Backend.Controllers.Requests;

public class smsRequest
{
    public int storeId {get; set;}
    public string targetNumber {get; set;}
    
    public smsRequest(int storeId, string targetNumber)
    {
        this.storeId = storeId;
        this.targetNumber = targetNumber;
    }
}