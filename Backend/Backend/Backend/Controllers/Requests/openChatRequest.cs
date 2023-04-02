namespace Backend.Controllers.Requests;

public class openChatRequest
{
    public int userId { get; set; }
    public int StoreId { get; set; }


    public openChatRequest(int userId,int StoreId)
    {
        this.userId=userId;
        this.StoreId=StoreId;
    }
}