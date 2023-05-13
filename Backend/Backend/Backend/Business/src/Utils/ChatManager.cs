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
        public static List<Chat> chats;
        private static int _nextSession = DBManager.Instance.getMaxChatId();

        public ChatManager(int id, bool isStore)
        {
            chats = new List<Chat>();
            List<Backend.Access.Chat> DALChats;
            if (isStore)
                DALChats = DBManager.Instance.LoadChatsPerStore(id);
            else
                DALChats = DBManager.Instance.LoadChatsPerUser(id);

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

        public Response<string> SendMessage(int sessionId, Message<String> message, bool fromStore)
        {
            Chat chat = chats.Find(x => x.sessionId == sessionId);
            if(chat is null)
                return new Response<string>(true, $"No chat exist with the session id of {sessionId}");
            chat.AddMessage(message);
            DBManager.Instance.AddMessage(chat.messages.Count -1, sessionId, fromStore, message.message);
            return new Response<string>(JsonConvert.SerializeObject(message));
        }
        
        public Response<bool> SendMessage(int sessionId, Message<House> message, bool fromStore)
        {
            Chat chat = chats.Find(x => x.sessionId == sessionId);
            if(chat is null)
                return new Response<bool>(false);
            chat.AddMessage(message);
            DBManager.Instance.AddMessage(chat.messages.Count -1, sessionId, fromStore, message.message);
            return new Response<bool>(true);
        }

        public Response<List<String>> GetAllStoreChats(int id)
        {
            List<Chat> allChats = chats.FindAll(x => x.store == id);
            List<string> allJsons = new List<string>();

            foreach (Chat chat in allChats)
            {
                allJsons.Add(chat.ToString());
            }

            return new Response<List<string>>(allJsons);
        }
        
        public Response<List<String>> GetAllUserChats(int id)
        {
            List<Chat> allChats = chats.FindAll(x => x.user == id);
            List<string> allJsons = new List<string>();

            foreach (Chat chat in allChats)
            {
                allJsons.Add(chat.ToString());
            }

            return new Response<List<string>>(allJsons);
        }

    }
}