using System;
using Backend.Business.src.Client_Store;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Backend.Business.Utils
{
    public abstract class User
    {
        int userId;
        int storeId;
        string name;
        string phoneNumber;
        
        protected User(int userId,int storeId,string name,string phoneNumber) {
            this.userId = userId;
            this.storeId = storeId;
            this.name = name;
            this.phoneNumber = phoneNumber;
        }

        //will be implmented in 
        public string getNotifction(Post post)
        {
            return "";
        }
    }
}