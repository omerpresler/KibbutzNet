using System;
using System.IO;
using System.Collections.Generic;
using Backend.Business.Utils;

namespace Backend.Business.src.Utils
{
    public class Chat
    {
        private List<IMessage> messages;
        public int sessionId { get; set;}
        private User source;
        private User target;
        public bool active { get; set;}
        private DateTime start;

        public Chat(int sessionId, User source, User target, bool active, DateTime start)
        {
            messages = new List<IMessage>();
            this.sessionId = sessionId;
            this.source = source;
            this.target = target;
            this.active = active;
            this.start = start;
        }
        

        public void AddMessage(string message)
        {
            messages.Add(new Message<Object>(source, message));
        }
    }
}