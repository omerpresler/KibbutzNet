namespace Backend.Business.src.Utils
{
    public class Message<T> : IMessage
    {
        public User sender { get; set; }
        public string message { get; set; }
        public T addon { get; set; }
        
        public Message(User sender, string message)
        {
            
            this.sender = sender;
            this.message = message;
        }
        
        public Message(User sender, string message, T addon)
        {
            this.sender = sender;
            this.message = message;
            this.addon = addon;
        }
    }
}