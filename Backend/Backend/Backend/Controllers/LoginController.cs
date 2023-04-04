using Microsoft.AspNetCore.Mvc;

using Backend.Controllers.Requests;
using Backend.Business.Utils;
using Backend.Business.src.Utils;

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
        public Response<bool> loginUser([FromBody] userLoginRequest request)
        {
            Console.WriteLine(request);
            Response<bool> res = new Response<bool>(true);
            return  res;
        }

        [HttpPost("loginToStore")]
        public Response<bool> loginStore([FromBody] storeLoginRequest request)
        {
            Console.WriteLine(request.storeId,request.email,request.storeId);
            Console.WriteLine(request.email, request.storeId);
            Console.WriteLine( request.storeId);
            Response<bool> res = new(true);

            return res;
        }



        //register -add new purchse see purchse history
        //store-client-get report 




    }
}