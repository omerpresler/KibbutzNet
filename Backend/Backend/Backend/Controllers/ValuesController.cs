using Microsoft.AspNetCore.Mvc;

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
        public bool login()
        {
            Console.WriteLine("yess!!!");
            return true;
        }

        



    }
}