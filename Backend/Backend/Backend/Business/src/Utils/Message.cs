using Backend.Business.Utils;
using Newtonsoft.Json;

namespace Backend.Business.src.Utils
{
    public class Message<T> : IMessage
    {
        public int sender { get; set; }
        public string message { get; set; }
        public T addon { get; set; }
        
        public Message(int sender, string message)
        {
            
            this.sender = sender;
            this.message = message;
        }
        
        public Message(int sender, string message, T addon)
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