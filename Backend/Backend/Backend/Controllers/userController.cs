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
        //int userId
        public Response<int> openChat([FromBody] openChatRequest request)
        {

        }
        [HttpPost("sendMassageInChat")]
        //json.stringfy(message)
        public Response<string> sendMassageInChat([FromBody] chatMassageRequest request)
        {

        }


        //list(json.stringfy(chats)
        [HttpPost("getAllchats")]
        public Response<List<String>> getAllchats([FromBody] storeLoginRequest request)
        {
            List<string> result = new List<string>();
            Response<List<string>> res = new(result);
            return res ;
        }
    }
}