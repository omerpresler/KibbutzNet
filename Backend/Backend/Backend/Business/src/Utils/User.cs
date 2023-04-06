using System;
using Backend.Business.src.Client_Store;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Backend.Business.Utils
{
    public abstract class User
    {
        public int UserId;
        string Name;
        string PhoneNumber;
        
        protected User(int userId ,string name,string phoneNumber) {
            this.UserId = userId;
            this.Name = name;
            this.PhoneNumber = phoneNumber;
        }

        //will be implmented in 
        public string getNotifction(Post post)
        {
            return "";
        }
    }
}