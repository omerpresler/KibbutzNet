using Microsoft.AspNetCore.Mvc;
using Backend.Business.src.Utils;
using Backend.Controllers.Requests;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {

        public StoreController()
        {
            //127.128.0.0 ()
        }
        //int StoreId string from string too
        [HttpPost("addPurchase")]
        public bool SeeReports([FromBody] PurchaseHistoryRequest request)
        {
            Console.WriteLine("yess!!!");
            //Console.WriteLine(request.email);
            return true;
        }

        //int StoreId 
        [HttpPost("sendbyemail")]
        public bool SendByEmail([FromBody] UserDataRequest request)
        {

            Console.WriteLine("yess!!!");
            Console.WriteLine(request.email);
            return true;
        }

        //int StoreId 
        [HttpPost("sendbysms")]
        public bool SendBySms([FromBody] UserDataRequest request)
        {

            Console.WriteLine("yess!!!");
            Console.WriteLine(request.email);
            return true;
        }
      

        [HttpPost("openChatStore")]
        public Response<int> openChat([FromBody] openChatRequest request)
        {

        }
        [HttpPost("sendMassageInChat")]
        public Response<int> sendMassageInChat([FromBody] chatMassageRequest request)
        {

        }
        //list (list massages ,int seesion id)
        [HttpPost("getAllchats")]
        public Response<List<String>> getAllchats([FromBody] storeLoginRequest request)
        {

        }

        // [HttpPost("closeChatStore")]
        // public Response<int> closeChat([FromBody] openChatRequest request)
        // {
            
        // }



    }
}