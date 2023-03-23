using System;
using Backend.Business.src.Client_Store;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Backend.Business.Utils
{
    public class User
    {
        int userId;
        string name;
        int acountNumber;
        string email;

        public User(int userId,string name, int acountNumber , string email )
        {
            this.userId = userId;
            this.name = name;
            this.acountNumber = acountNumber;
            this.email = email;
        }

        public bool checkEquelUser(int userId,string email)
        {
            return userId==this.userId && email==this.email;   
        }
        //will be implmented in 
        public string getNotifction(Post post)
        {
            return "";
        }
    }
}