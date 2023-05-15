using System;
using Backend.Business.src.Client_Store;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Backend.Business.Utils
{
    public abstract class User
    {
        public int UserId;
        public string Name;
        public string PhoneNumber;
        public string email;
        
        protected User(int userId ,string name,string phoneNumber, string email) {
            this.UserId = userId;
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.email = email;
        }

        //will be implmented in 
        public string getNotifction(Post post)
        {
            return "";
        }
    }
}