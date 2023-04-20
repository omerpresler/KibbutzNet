using Microsoft.AspNetCore.Mvc;
using Backend.Business.src.Utils;
using Backend.Controllers.Requests;
using Backend.Service;

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
        [HttpPost("loginToStore")]
        public Response<bool> Login([FromBody] storeLoginRequest request)
        {
            return Store.Instance.Login(request.accountNumber, request.storeId, request.password);
        }
        
        //int StoreId string from string too
        [HttpPost("SeeReports")]
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
        //int userId
        public Response<int> openChat([FromBody] openChatRequest request)
        {
            return Store.Instance.OpenChat(request.StoreId, request.userId);
        }
        [HttpPost("sendMassageInChat")]
        //json.stringfy(message)
        public Response<string> sendMassageInChat([FromBody] chatMassageRequest request)
        {
            return Store.Instance.SendMessage(request.StoreId, request.SessionId, request.Text);
        }
        //list(json.stringfy(chats)
        [HttpPost("getAllchats")]
        public Response<List<String>> getAllchats(int StoreId)
        {
            return Store.Instance.GetAllchats(StoreId);
        }



        [HttpPost("getPurchaseHistory")]
        public Response<List<String>> getAllchats(int StoreId)
        {
            return Store.Instance.GetAllchats(StoreId);
        }

        // [HttpPost("closeChatStore")]
        // public Response<int> closeChat([FromBody] openChatRequest request)
        // {

        // }



    }
}