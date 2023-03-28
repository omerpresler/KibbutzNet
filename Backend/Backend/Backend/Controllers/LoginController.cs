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

        [HttpPost("login")]
        public bool login([FromBody] UserDataRequest request)
        {
            Console.WriteLine(request.email);
            Console.WriteLine(request.accountNumber);
            return true;
        }
        //register -add new purchse see purchse history
        //store-client-get report 
    



    }
}