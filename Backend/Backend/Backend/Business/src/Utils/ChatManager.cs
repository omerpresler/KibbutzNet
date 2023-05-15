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

        public ChatManager() { }

        public static void LoadChats()
        {
            List<Backend.Access.Chat> DALChats;
            DALChats = DBManager.Instance.LoadChats();

            foreach (Backend.Access.Chat chat in DALChats)
            {
                chats.Add(new Chat(chat, DBManager.Instance.LoadMessagesPerChat(chat.user, chat.store)));
            }
        }
        
        

        public List<Chat> GetChats()
        {return chats;}

        public bool chatExist(int userId, int storeId)
        {
            foreach (Chat chat in chats)
                if (chat.store == storeId && chat.user == userId)
                    return true;

            return false;
        }

        public Response<string> SendMessage(int userId, int storeId, Message message, bool fromStore)
        {
            Chat? chat = chats.Find(x => (x.user == userId) && (x.store == storeId));
            if (chat is null)
            {
                chat = new Chat(storeId, userId, true, DateTime.Now);
                chats.Add(chat);
                DBManager.Instance.AddChat(storeId, userId, chat.active, chat.start);
            }
            
            
            chat.AddMessage(message);
            DBManager.Instance.AddMessage(chat.messages.Count -1, userId, storeId, fromStore, message.message);
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