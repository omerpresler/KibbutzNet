using Backend.Business.Utils;
using Newtonsoft.Json;

namespace Backend.Business.src.Utils
{
    public class Message<T> : IMessage
    {
        public virtual int messageID { get; set; }
        public virtual User sender { get; set; }
        public virtual string message { get; set; }
        public T addon { get; set; }
        public virtual DateTime date { get; }
        
        public Message(User sender, string message)
        {
            this.sender = sender;
            this.message = message;
            date = DateTime.Now;
        }
        
        public Message(User sender, string message, T addon)
        {
            this.sender = sender;
            this.message = message;
            this.addon = addon;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}