using Microsoft.AspNetCore.Mvc;

using Controllers.Requests;

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
           
            Console.WriteLine("yess!!!");
            Console.WriteLine(request.email);
            return true;
        }

        



    }
}