using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Backend.Business.src.Utils;
using Backend.Controllers.Requests;
using Backend.Service;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class userController : ControllerBase
    {
        [HttpPost("openChatUser")]
        //int userId
        public Response<int> openChat([FromBody] openChatRequest request)
        {
            int result = 7;
            Response<int> res = new (result);
            return res;
        }
        [HttpPost("sendMassageInChat")]
        //json.stringfy(message)
        public Response<string> sendMassageInChat([FromBody] chatMassageRequest request){
            string result = "7";
            Response<string> res = new(result);
            return res;
        }

        //list Json
        //PurchaseID" : Number,"Date": "dd/mm/yyy","BudgetNumber" : Number,"EmployeeID" : Number,"Cost" : Number,"Description" : "......"
        [HttpPost("SeeReports")]
        public bool SeeReports([FromBody] PurchaseHistoryRequest request)
        {
            Console.WriteLine("yess!!!");
            //Console.WriteLine(request.email);
            return true;
        }
        //list(json.stringfy(chats)
        [HttpPost("getAllchats")]
        public Response<List<String>> getAllchats([FromBody] storeLoginRequest request)
        {
            List<string> result = new List<string>();
            Response<List<string>> res = new(result);
            return res ;
        }
        
        [HttpPost("SeePurchaseHistoryUser")]
        public Response<ArrayList> SeePurchaseHistoryUser([FromBody] PurchaseHistoryRequest request)
        {
            return Store.Instance.SeePurchaseHistoryUser(request.UserId);
        }

    }
}