namespace Backend.Access;

public class Chat
{
    public int sessionId { get; set;}
    public int store { get; set;}
    public int user { get; set;}
    public bool active { get; set;}
    public DateTime start { get; set;}
    
    public Chat(DateTime start, int sessionId, int store, int user, bool active)
    {
        this.start = start;
        this.sessionId = sessionId;
        this.store = store;
        this.user = user;
        this.active = active;
    }
}