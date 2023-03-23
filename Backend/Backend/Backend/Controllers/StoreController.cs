using Microsoft.AspNetCore.Mvc;

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
            Console.WriteLine(request.email);
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
      





    }
}