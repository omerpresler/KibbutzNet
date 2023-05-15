namespace Backend.Access;

public class Chat
{
    public int store { get; set;}
    public int user { get; set;}
    public bool active { get; set;}
    public DateTime start { get; set;}
    
    public Chat(int store, int user, bool active, DateTime start)
    {
        this.start = start;
        this.store = store;
        this.user = user;
        this.active = active;
    }
}