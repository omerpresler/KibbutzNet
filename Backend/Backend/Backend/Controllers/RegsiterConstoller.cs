using Microsoft.AspNetCore.Mvc;

using Controllers.Requests;

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
        public bool addPurchase([FromBody] UserDataRequest request)
        {

            Console.WriteLine("yess!!!");
            Console.WriteLine(request.email);
            return true;
        }
        //int StoreId 
        [HttpPost("addPurchase")]
        public bool SeePurchaseHistory([FromBody] UserDataRequest request)
        {

            Console.WriteLine("yess!!!");
            Console.WriteLine(request.email);
            return true;
        }
        //register -add new purchse see purchse history
        //store-client-get report 




    }
}