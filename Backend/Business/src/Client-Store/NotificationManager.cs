using System.Collections.Generic;
using Backend.Business.Utils;

namespace Backend.Business.src.Client_Store
{
    //visitor pattern
    public class NotificationManager
    {
        private List<User> listeners;

        public string addListner(User user)
        {
            if (!listeners.Contains(user))
            {
                listeners.Add(user);
                return "the user was added to the notifction list";
            }
            return "the user is aאכל lready in the notifction list";
        }

        public string removeListiner(User user)
        {
            if (listeners.Contains(user))
            {
                listeners.Add(user);
                return "the user was removed to the notifction list";
            }
            return "the user is not in the notifction list";
        }
        public string notify(Post post){
            foreach (var lisiner in listeners)
            {
                lisiner.getNotifction(post);
            }
            return "all users get the push broadcast massage";
        }
        
    }
}