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
        public int store;
        public int user;
        public bool active { get; set;}
        private DateTime start;

        public Chat(int sessionId, int store, int user, bool active, DateTime start)
        {
            messages = new List<IMessage>();
            this.sessionId = sessionId;
            this.store = store;
            this.user = user;
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