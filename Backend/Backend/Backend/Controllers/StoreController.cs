using Microsoft.AspNetCore.Mvc;
using Backend.Business.src.Utils;
using Backend.Controllers.Requests;
using Backend.Service;
using System.Collections;


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
            return Store.Instance.Login(request.accountNumber, request.storeId, request.email);
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



        [HttpPost("addOrder")]
        public Response<int> addOrder([FromBody] newOrderRequest request)
        {
            return Store.Instance.addOrder(request.storeId, request.memberId, request.memberName, request.description, request.cost);
        }

        

        [HttpPost("changeOrdersStatus")]
        public Response<string> changeOrdersStatus([FromBody] changeOrdersStatusRequest request)
        {
            return Store.Instance.changeOrdersStatus(request.storeId, request.orderId, request.status);
        }
        
        [HttpPost("addPurchase")]
        public Response<int> addPurchase([FromBody] newPurchaseRequest request)
        {
            return Store.Instance.addPurchase(request.storeId, request.memberId,request.description, request.cost);
        }
        

        //int StoreId 
        [HttpPost("seeOrderHistoryStore")]
        public Response<ArrayList> seeOrderHistoryStore([FromBody] PurchaseHistoryRequest request)
        {
            return Store.Instance.SeeOrderHistoryStore(request.StoreId);
        }



        [HttpPost("seeOrderHistoryUserAndStore")]
        public Response<ArrayList> seeOrderHistoryUserAndStore([FromBody] PurchaseHistoryRequest request)
        {
            return Store.Instance.SeeOrderHistoryUserAndStore(request.StoreId, request.UserId);
        }
        
        
        
        [HttpPost("seePurchaseHistoryStore")]
        public Response<ArrayList> seePurchaseHistoryStore([FromBody] PurchaseHistoryRequest request)
        {
            return Store.Instance.SeePurchaseHistoryStore(request.StoreId);
        }



        [HttpPost("seePurchaseHistoryUserAndStore")]
        public Response<ArrayList> seePurchaseHistoryUserAndStore([FromBody] PurchaseHistoryRequest request)
        {
            return Store.Instance.SeePurchaseHistoryUserAndStore(request.StoreId, request.UserId);
        }



    }
}