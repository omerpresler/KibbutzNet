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

        [HttpPost("test")]
        public bool test()
        {
            return true;
        }

    }
}