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
        public bool login(string email,int acountNum)
        {
            Console.WriteLine(email, acountNum);
            return true;
        }

        



    }
}