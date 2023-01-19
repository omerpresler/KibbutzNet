using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesForecastController : ControllerBase
    {

        private readonly ILogger<ValuesForecastController> _logger;

        public ValuesForecastController(ILogger<ValuesForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetValues")]
        public string Get()
        {
            return "1234";
        }

        [HttpGet("GetIndex")]
        public string Index()
        {
            return "12345";
        }

        [HttpPost("PostIndex")]
        public string post()
        {
            return "123456";
        }



    }
}