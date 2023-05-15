using Microsoft.AspNetCore.Mvc;
using Backend.Business.src.Utils;
using Backend.Controllers.Requests;
using Backend.Service;
using System.Collections;
using Backend.Business.src.Client_Store;


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
        [HttpPost("SeeReports")]
        public bool SeeReports([FromBody] PurchaseHistoryRequest request)
        {
            Console.WriteLine("yess!!!");
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

        
        [HttpPost("closeOrder")]
        public Response<bool> closeOrder([FromBody] changeOrdersActiveStateRequest request)
        {
            return Store.Instance.closeOrder(request.storeId, request.orderId);
        }

        [HttpPost("reOpenOrder")]
        public Response<bool> reOpenOrder([FromBody] changeOrdersActiveStateRequest request)
        {
            return Store.Instance.reOpenOrder(request.storeId, request.orderId);
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

        [HttpPost("addPost")]
        public Response<Post> AddPost(int storeId, String header)
        {
            return Store.Instance.AddPost(storeId, header);
        }
        
        [HttpPost("removePost")]
        public Response<Post> RemovePost(int storeId, int postId)
        {
            return Store.Instance.RemovePost(storeId, postId);
        }
        
        [HttpPost("addProduct")]
        public Response<Product> AddProduct(int storeId, string name, string description)
        {
            return Store.Instance.AddProduct(storeId, name, description);
        }
        
        [HttpPost("removeProduct")]
        public Response<Product> RemoveProduct(int storeId, int productId)
        {
            return Store.Instance.RemoveProduct(storeId, productId);
        }

    }
}