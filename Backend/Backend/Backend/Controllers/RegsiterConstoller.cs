using Microsoft.AspNetCore.Mvc;

using Controllers.Requests;
using Backend.Controllers.Requests;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {



        public RegisterController()
        {
            //127.128.0.0 ()
        }
        //float price string description int budget number int storeId int EmplooId 
        [HttpPost("addPurchase")]
        public bool addPurchase([FromBody] purchaseDataRequest request)
        {

            Console.WriteLine("yess!!!");
            Console.WriteLine(request.price);
            return true;
        }
        //int StoreId 
        [HttpPost("seePurchaseHistory")]
        public bool SeePurchaseHistory([FromBody] purchaseHistoryRequest request)
        {

            Console.WriteLine("yess!!!");
            Console.WriteLine(request.from);
            Console.WriteLine(request.to);
            return true;
        }
        //register -add new purchse see purchse history
        //store-client-get report 




    }
}