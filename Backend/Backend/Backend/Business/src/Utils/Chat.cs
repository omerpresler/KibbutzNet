using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using Backend.Business.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Backend.Business.src.Utils
{
    public class Chat
    {
        public List<Message> messages;
        public int store { get; set;}
        public int user { get; set;}
        public bool active { get; set;}
        public DateTime start { get; set;}

        public Chat( int store, int user, bool active, DateTime start)
        {
            messages = new List<Message>();
            this.store = store;
            this.user = user;
            this.active = active;
            this.start = start;
        }
        
        public Chat(Access.Chat chat, List<Access.Message> messages)
        {
            this.store = chat.store;
            this.user = chat.user;
            this.active = chat.active;
            this.start = chat.start;
            this.messages = new List<Message>();

            foreach (Access.Message msg in messages.OrderBy(x => x.placeInChat))
            {
                Message msgToAdd = new Message(msg);
                this.messages.Add(msgToAdd);
            }
        }
        

        public void AddMessage(Message message)
        {
            messages.Add(message);
        }

        public string ToString(bool isStore)
        {
            List<Object> msgs = new List<object>();

            foreach (Message msg in messages)
            {
                msgs.Add(new
                {
                    message = msg.message,
                    FromMe = !(isStore ^ msg.fromStore)
                });
            }

            var simpleChat = new
            {
                Store = store,
                User = user,
                Messages = msgs,
            };
            
            return JsonSerializer.Serialize(simpleChat);
        }
    }
}