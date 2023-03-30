using Microsoft.AspNetCore.Mvc;

using Backend.Controllers.Requests;
namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class loginController : ControllerBase
    {

      

        public loginController()
        {
        }


        [HttpPost("loginToUser")]
        public response<bool> loginUser([FromBody] userLoginRequest request)
        {
            Console.WriteLine(request);
            response<bool> res = new response<bool>(true,false);
            return  res;
        }

        [HttpPost("loginToStore")]
        public response<bool> loginStore([FromBody] storeLoginRequest request)
        {
            Console.WriteLine(request.storeId,request.email,request.storeId);
            Console.WriteLine(request.email, request.storeId);
            Console.WriteLine( request.storeId);
            response<bool> res = new(true, false);

            return res;
        }



        //register -add new purchse see purchse history
        //store-client-get report 




    }
}