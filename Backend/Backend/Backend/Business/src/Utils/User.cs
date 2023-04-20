using System;
using Backend.Business.src.Client_Store;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Backend.Business.Utils
{
    public abstract class User
    {
        public virtual int userId { get; set; }
        public virtual string name { get; set; }
        public string phoneNumber { get; set; }
        
        protected User(int userId,int storeId,string name,string phoneNumber) {
            this.userId = userId;
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