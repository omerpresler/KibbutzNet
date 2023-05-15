namespace Backend.Access;

public class Message
{
    public int placeInChat { get; set; }
    public int userId { get; set; }
    public int storeId { get; set; }
    public bool fromStore { get; set; }
    public string message { get; set; }
    
    public Message(int placeInChat, int userId, int storeId, bool fromStore, string message)
    {
        this.placeInChat = placeInChat;
        this.userId = userId;
        this.storeId = storeId;
        this.fromStore = fromStore;
        this.message = message;
    }
}