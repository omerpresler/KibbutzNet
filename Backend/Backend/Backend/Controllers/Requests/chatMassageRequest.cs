namespace Backend.Controllers.Requests;

public class chatMassageRequest
{
    public int id {get; set;}
    
    public int SessionId {get; set;}

    public string Text {get; set;}


    



    public chatMassageRequest(int id, int sessionId,string text)
    {
        this.id = id;
        this.SessionId = sessionId;
        this.Text = text;
    }
}