<<<<<<< HEAD:Amit-Backend/Business/src/Client-Store/StoreController.cs
using Backend.Business.Client_Store;
using Backend.Business.Utils;
using Backend.Business.Reports;
=======
using Backend.Business.src.Utils;
using Backend.Business.src.Reports;
>>>>>>> 552acd561834f8a366ba1d338b881ff826276c4e:rotem/KibbutzNet/Backend/Business/src/Client-Member/StoreController.cs

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
<<<<<<< HEAD:Amit-Backend/Business/src/Client-Store/StoreController.cs
            return new Response<bool>();
=======
            
            return new Response<bool>(true);
>>>>>>> 552acd561834f8a366ba1d338b881ff826276c4e:rotem/KibbutzNet/Backend/Business/src/Client-Member/StoreController.cs
        }

        public Response<bool> startChat(int member)
        {
<<<<<<< HEAD:Amit-Backend/Business/src/Client-Store/StoreController.cs
            return new Response<bool>();
=======

            return new Response<bool>(true);
>>>>>>> 552acd561834f8a366ba1d338b881ff826276c4e:rotem/KibbutzNet/Backend/Business/src/Client-Member/StoreController.cs
        }

        public Response<bool> endChat(int member)
        {
<<<<<<< HEAD:Amit-Backend/Business/src/Client-Store/StoreController.cs
            return new Response<bool>();
=======

            return new Response<bool>(true);
>>>>>>> 552acd561834f8a366ba1d338b881ff826276c4e:rotem/KibbutzNet/Backend/Business/src/Client-Member/StoreController.cs
        }
    }
}