namespace Backend.Access;

public class Message
{
    public int placeInChat { get; set; }
    public int chat { get; set; }
    public bool fromStore { get; set; }
    public string message { get; set; }
    
    public Message(int placeInChat, int chat, bool fromStore, string message)
    {
        this.placeInChat = placeInChat;
        this.chat = chat;
        this.fromStore = fromStore;
        this.message = message;
    }
}