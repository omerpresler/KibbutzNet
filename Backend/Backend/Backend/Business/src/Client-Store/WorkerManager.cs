using System.Collections.Generic;
using System.Threading;
using Backend.Business.Utils;

namespace Backend.Business.src.Client_Store
{
    public class WorkerManager
    {
        public List<User> workers { get; set; }
        private static int nextUserId;

        public WorkerManager()
        {
            this.workers = new List<User>();
        }
        
        private static int AssignSession()
        {
            return Interlocked.Increment(ref nextUserId);
        }

        public bool WorkerExist(User user)
        {
            foreach (User worker in workers)
                if (worker.Equals(user))
                    return true;

            return false;
        }
    }
}