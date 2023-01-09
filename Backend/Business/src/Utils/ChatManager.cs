using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Backend.Business.src.Utils
{
    public class ChatManager
    {
        private List<Chat> chats;
        private static int nextSession;

        public ChatManager()
        {
            this.chats = new List<Chat>();
        }

        private static int AssignSession()
        {
            return Interlocked.Increment(ref nextSession);
        }

        public Response<bool> StartChat(User sender, User target)
        {
            try
            {
                Chat chat = new Chat(AssignSession(), sender, target, true, DateTime.Now);
            }
            catch (Exception e)
            {
                return new Response<bool>(true, e.Message);
            }
            
            
            return new Response<bool>(true);
        }
        
        public Response<bool> EndChat(int sessionId)
        {
            try
            {
                Chat toClose = chats.Find(x => x.sessionId == sessionId);
                return toClose.active ? new Response<bool>(true) : new Response<bool>(false, "Chat already closed");
            }
            catch (Exception e)
            {
                return new Response<bool>(true, e.Message);
            }
        }

        public Response<bool> SendMessage(int sessionId, string message)
        {
            Chat chat = chats.Find(x => x.sessionId == sessionId);
            if(chat is null)
                return new Response<bool>(false);
            chat.AddMessage(message);
            return new Response<bool>(true);
        }
    }
}