using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using Backend.Business.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;

namespace Backend.Business.src.Utils
{
    public class Chat
    {
        public List<Message> messages;
        public int sessionId { get; set;}
        public int store { get; set;}
        public int user { get; set;}
        public bool active { get; set;}
        public DateTime start { get; set;}

        public Chat(int sessionId, int store, int user, bool active, DateTime start)
        {
            messages = new List<Message>();
            this.sessionId = sessionId;
            this.store = store;
            this.user = user;
            this.active = active;
            this.start = start;
        }
        
        public Chat(Access.Chat chat, List<Access.Message> messages)
        {
            this.sessionId = chat.sessionId;
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
            string simpleChat = "{sessionId: " + sessionId + "}, storeId: {" + store + "}, userId: {" + user + "}, Messages: {";

            foreach (Message msg in messages)
                simpleChat += $"(message: {msg.message}, FromMe: {!(isStore ^ msg.fromStore)}), ";

            simpleChat += "}";
            
            return simpleChat;
        }
    }
}