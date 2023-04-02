namespace Backend.Controllers.Requests;

public class chatMassageRequest
{

    public int sessionId {get; set;}

    public string Text {get; set;}


    



    public chatMassageRequest(int seesionId,string Text)
    {
        this.sessionId=sessionId;
        this.Text=Text;
    }
}