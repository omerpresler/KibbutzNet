namespace Backend.Controllers.Requests;

public class chatMassageRequest
{
    public int userId {get; set;}
    
    public int storeId {get; set;}

    public string text {get; set;}


    



    public chatMassageRequest(int userId, int storeId,string text)
    {
        this.userId = userId;
        this.storeId = storeId;
        this.text = text;
    }
}