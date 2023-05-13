using Backend.Business.Utils;
using Newtonsoft.Json;

namespace Backend.Business.src.Utils
{
    public class Message<T> : IMessage
    {
        //represent if the store or the user sent it
        public bool fromStore { get; set; }
        public string message { get; set; }
        public T addon { get; set; }
        
        public Message(bool fromStore, string message)
        {
            
            this.fromStore = fromStore;
            this.message = message;
        }
        
        public Message(bool fromStore, string message, T addon)
        {
            this.fromStore = fromStore;
            this.message = message;
            this.addon = addon;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}