using Microsoft.AspNetCore.Mvc;
using Backend.Controllers.Requests;
using Backend.Business.Utils;
using Backend.Business.src.Utils;
using Backend.Service;

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
            return Service.User.Instance.Login(request.accountNumber, request.email);
        }

        [HttpPost("loginToStore")]
        public Response<bool> loginStore([FromBody] storeLoginRequest request)
        {
            Console.WriteLine(request);
            return Store.Instance.Login(request.accountNumber, request.storeId, request.email);
        }
        
        [HttpPost("loginToRegister")]
        public Response<String> loginRegister([FromBody] RegisterInfoRequest request)
        {
            return Register.Instance.OpenRegister(request.StoreId, request.EmployeeId);
        }



        //register -add new purchse see purchse history
        //store-client-get report 




    }
}