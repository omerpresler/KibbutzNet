using Backend.Business.src.Utils;
using Backend.Controllers.Requests;
using Backend.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class userController : ControllerBase
    {
        [HttpPost("sendMassageInChat")]
        //json.stringfy(message)
        public Response<string> sendMassageInChat([FromBody] chatMassageRequest request)
        {
            return Service.User.Instance.SendMessage(request.userId, request.storeId, request.text);
        }

        //list Json
        //PurchaseID" : Number,"Date": "dd/mm/yyy","BudgetNumber" : Number,"EmployeeID" : Number,"Cost" : Number,"Description" : "......"
        [HttpPost("seeReports")]
        public bool SeeReports([FromBody] PurchaseHistoryRequest request)
        {
            Console.WriteLine("yess!!!");
            //Console.WriteLine(request.email);
            return true;
        }
        
        
        [HttpPost("getAllchats")]
        public Response<List<String>> getAllchats([FromBody] userIdRequest request)
        {

            return Service.User.Instance.GetAllchats(request.userId);
        }
        
        [HttpPost("seeOrderHistoryUser")]
        public Response<ArrayList> seeOrderHistoryUser(int userId)
        {
            return Store.Instance.SeeOrderHistoryUser(userId);
        }
        
        [HttpPost("seeOrderHistoryUserAndStore")]
        public Response<ArrayList> seeOrderHistoryUserAndStore([FromBody] PurchaseHistoryRequest request)
        {
            return Store.Instance.SeeOrderHistoryUserAndStore(request.StoreId, request.UserId);
        }
        
        [HttpPost("seePurchaseHistoryUser")]
        public Response<ArrayList> seePurchaseHistoryUser(int userId)
        {
            return Store.Instance.SeePurchaseHistoryUser(userId);
        }
        
        [HttpPost("seePurchaseHistoryUserAndStore")]
        public Response<ArrayList> seePurchaseHistoryUserAndStore([FromBody] PurchaseHistoryRequest request)
        {
            return Store.Instance.SeePurchaseHistoryUserAndStore(request.StoreId, request.UserId);
        }
        
        [HttpPost("getAllStores")]
        public Response<List<Tuple<int, String?, String?>>> getAllStores()
        {
            return Store.Instance.getAllStores();
        }
    }
}