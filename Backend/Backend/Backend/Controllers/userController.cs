using Microsoft.AspNetCore.Mvc;
using Backend.Business.src.Utils;
using Backend.Controllers.Requests;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class userController : ControllerBase
    {
        [HttpPost("openChatUser")]
        public Response<int> openChat([FromBody] openChatRequest request)
        {

        }

        [HttpPost("sendMassageInChat")]
        public Response<int> sendMassageInChat([FromBody] chatMassageRequest request)
        {

        }


        [HttpPost("getAllchats")]
        public Response<int> getAllchats([FromBody] UserDataRequest request)
        {

        }
    }
}