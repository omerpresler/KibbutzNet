using System;
using System.Collections;
using System.Collections.Generic;

namespace Backend.Business.src.Utils
{
    public class ChatManager
    {
        private List<Chat> chats;
        private int sessionId;
        private User member;
        private User store;
        private bool active;
        public DateTime start;
    }
}