using Backend.Business.Utils;
using Newtonsoft.Json;

namespace Backend.Business.src.Utils
{
    public class Message
    {
        //represent if the store or the user sent it
        public bool fromStore { get; set; }
        public string message { get; set; }
        
        public Message(Access.Message msg)
        {
            
            this.fromStore = msg.fromStore;
            this.message = msg.message;
        }
        
        public Message(bool fromStore, string message)
        {
            
            this.fromStore = fromStore;
            this.message = message;
        }
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}