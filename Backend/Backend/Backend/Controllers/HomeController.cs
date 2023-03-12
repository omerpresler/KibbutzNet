using Microsoft.AspNetCore.Mvc;

using Backend.Controllers.Requests;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {



        public HomeController()
        {
        }

        [HttpPost("openRegsiter")]
        public bool openRegsiter([FromBody] UserDataRequest request)
        {
            Console.WriteLine("register opend");
            return true;
        }
        [HttpPost("openStoreMember")]
        public bool openStoreMember([FromBody] UserDataRequest request)
        {
            Console.WriteLine("store membet opend");
            return true;
        }

        [HttpPost("openStoreOwner")]
        public bool openStoreOwner([FromBody] UserDataRequest request)
        {
            Console.WriteLine("store owner opend");
            return true;
        }

        //register -add new purchse see purchse history
        //store-client-get report 




    }
}