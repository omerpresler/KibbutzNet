using Backend.Business.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
namespace Backend.Business.src.Utils
{
    public class ChatManager
    {
        public static List<Chat> chats = new List<Chat>();
        private static int _nextSession;

        public List<Chat> GetChats()
        {return chats;}

        private static int AssignSession()
        {
            return Interlocked.Increment(ref _nextSession);
        }

        public Response<int> StartChat(User sender, User target)
        {
            Chat chat;
            try
            {
                chat = new Chat(AssignSession(), sender, target, true, DateTime.Now);
                chats.Add(chat);
            }
            catch (Exception e)
            {
                return new Response<int>(true, e.Message);
            }
            
            
            return new Response<int>(chat.sessionId);
        }
        
        public Response<bool> EndChat(int sessionId)
        {
            try
            {
                Chat toClose = chats.Find(x => x.sessionId == sessionId);
                if (toClose != null)
                {
                    //chats.Remove(toClose);
                    if (toClose.active)
                    {
                        toClose.active = false;
                        return new Response<bool>(true);
                    }
                    return new Response<bool>(true, "Chat already closed");
                }

                return new Response<bool>(true, "No such chat was found, id: " + sessionId);
            }
            catch (Exception e)
            {
                return new Response<bool>(true, e.Message);
            }
        }

        public Response<bool> SendMessage(int sessionId, Message<String> message)
        {
            Chat chat = chats.Find(x => x.sessionId == sessionId);
            if(chat is null)
                return new Response<bool>(false);
            chat.AddMessage(message);
            return new Response<bool>(true);
        }
        
        public Response<bool> SendMessage(int sessionId, Message<House> message)
        {
            Chat chat = chats.Find(x => x.sessionId == sessionId);
            if(chat is null)
                return new Response<bool>(false);
            chat.AddMessage(message);
            return new Response<bool>(true);
        }
    }
}