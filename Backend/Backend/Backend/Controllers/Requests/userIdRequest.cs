namespace Backend.Controllers.Requests;

public class userIdRequest
{

    public int userId { get; set; }


    public userIdRequest(int userId)
    {
        this.userId = userId;
    }
}