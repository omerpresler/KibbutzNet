using Backend.Business.src.Utils;
using Backend.Business.src.Reports;

namespace Backend.Business.src.Client_Store
{
    public class StoreController
    {
        private ChatManager chatManager;
        private OrderManager orderManager;
        private OutputManager outputManager;
        private WorkerManager workerManager;
        private User employee;
        private NotificationManager notificationManager;


        public Response<bool> logIn(string name, string pasword)
        {
            
            return new Response<bool>(true);
        }
        
        public Response<bool> startChat(int member)
        {

            return new Response<bool>(true);
        }
        
        public Response<bool> endChat(int member)
        {

            return new Response<bool>(true);
        }
    }
}