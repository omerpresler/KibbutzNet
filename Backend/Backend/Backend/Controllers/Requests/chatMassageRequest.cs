namespace Backend.Controllers.Requests;

public class chatMassageRequest
{
    public int StoreId {get; set;}
    
    public int SessionId {get; set;}

    public string Text {get; set;}


    



    public chatMassageRequest(int storeId, int sessionId,string text)
    {
        this.StoreId = storeId;
        this.SessionId = sessionId;
        this.Text = text;
    }
}