using System;
using Backend.Business.src.Client_Store;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Backend.Business.Utils
{
    public abstract class User
    {

        public virtual int UserId { get; set; }
        public virtual string Name { get; set; }
        public string PhoneNumber { get; set; }

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