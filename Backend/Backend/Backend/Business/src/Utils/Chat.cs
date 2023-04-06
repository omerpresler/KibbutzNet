using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using Backend.Business.Utils;
using Newtonsoft.Json;

namespace Backend.Business.src.Utils
{
    public class Chat
    {
        public List<IMessage> messages;
        public int sessionId { get; set;}
        private int source;
        private int target;
        public bool active { get; set;}
        private DateTime start;

        public Chat(int sessionId, int source, int target, bool active, DateTime start)
        {
            messages = new List<IMessage>();
            this.sessionId = sessionId;
            this.source = source;
            this.target = target;
            this.active = active;
            this.start = start;
        }
        

        public void AddMessage(Message<string> message)
        {
            messages.Add(message);
        }
        
        public void AddMessage(Message<House> message)
        {
            messages.Add(message);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}