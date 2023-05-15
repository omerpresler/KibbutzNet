using Backend.Business.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Backend.Access;

namespace Backend.Business.src.Utils
{
    public class ChatManager
    {
        public static List<Chat> chats = new List<Chat>();
        private static int _nextSession = DBManager.Instance.getMaxChatId();

        public ChatManager() { }

        public static void LoadChats()
        {
            List<Backend.Access.Chat> DALChats;
            DALChats = DBManager.Instance.LoadChats();

            foreach (Backend.Access.Chat chat in DALChats)
            {
                chats.Add(new Chat(chat, DBManager.Instance.LoadMessagesPerChat(chat.sessionId)));
            }
        }

        public List<Chat> GetChats()
        {return chats;}

        private static int AssignSession()
        {
            return Interlocked.Increment(ref _nextSession);
        }

        public Response<int> StartChat(int store, int user)
        {
            Chat chat;
            try
            {
                chat = new Chat(AssignSession(), store, user, true, DateTime.Now);
                chats.Add(chat);
                DBManager.Instance.AddChat(chat.sessionId, store, user, chat.active, chat.start);
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
                        DBManager.Instance.updateChatActiveField(sessionId, false);
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

        public Response<string> SendMessage(int sessionId, Message message, bool fromStore)
        {
            Chat chat = chats.Find(x => x.sessionId == sessionId);
            if(chat is null)
                return new Response<string>(true, $"No chat exist with the session id of {sessionId}");
            chat.AddMessage(message);
            DBManager.Instance.AddMessage(chat.messages.Count -1, sessionId, fromStore, message.message);
            return new Response<string>(JsonConvert.SerializeObject(message));
        }


        public Response<List<String>> GetAllStoreChats(int id)
        {
            List<Chat> allChats = chats.FindAll(x => x.store == id);
            List<string> allJsons = new List<string>();

            foreach (Chat chat in allChats)
            {
                allJsons.Add(chat.ToString(true));
            }

            return new Response<List<string>>(allJsons);
        }
        
        public Response<List<String>> GetAllUserChats(int id)
        {
            List<Chat> allChats = chats.FindAll(x => x.user == id);
            List<string> allJsons = new List<string>();

            foreach (Chat chat in allChats)
            {
                allJsons.Add(chat.ToString(false));
            }

            return new Response<List<string>>(allJsons);
        }

    }
}