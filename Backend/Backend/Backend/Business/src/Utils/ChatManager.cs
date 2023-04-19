using Backend.Business.Utils;
using Newtonsoft.Json;
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

        public Response<int> StartChat(int sender, int target)
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

        public Response<string> SendMessage(int sessionId, Message<String> message)
        {
            Chat chat = chats.Find(x => x.sessionId == sessionId);
            if(chat is null)
                return new Response<string>(true, $"No chat exist with the session id of {sessionId}");
            chat.AddMessage(message);
            return new Response<string>(JsonConvert.SerializeObject(message));
        }
        
        public Response<bool> SendMessage(int sessionId, Message<House> message)
        {
            Chat chat = chats.Find(x => x.sessionId == sessionId);
            if(chat is null)
                return new Response<bool>(false);
            chat.AddMessage(message);
            return new Response<bool>(true);
        }

        public Response<List<String>> GetAllchatsStore(int storeId)
        {
            List<Chat> allChats = chats.FindAll(x => x.source == storeId | x.target == storeId);
            List<string> allJsons = new List<string>();

            foreach (Chat chat in allChats)
            {
                allJsons.Add(chat.ToString());
            }

            return new Response<List<string>>(allJsons);
        }


        public Response<List<String>> GetAllchatsUser(int userId)
        {
            List<Chat> allChats = chats.FindAll(x => x.source == userId | x.target == userId);
            List<string> allJsons = new List<string>();

            foreach (Chat chat in allChats)
            {
                allJsons.Add(chat.ToString());
            }

            return new Response<List<string>>(allJsons);
        }
    }
}