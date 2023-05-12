﻿using Backend.Access;
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
        public loginController() { }
        
        
        
        [HttpPost("loginToAdmin")]
        public Response<bool> loginToAdmin([FromBody] userLoginRequest request)
        {
            return Service.Admin.Instance.Login(request.accountNumber, request.email);
        }
        
        [HttpPost("loginToUser")]
        public Response<bool> loginUser([FromBody] userLoginRequest request)
        {
            Console.WriteLine("test");
            Console.WriteLine(request);
            return Service.User.Instance.Login(request.accountNumber, request.email);
        }

        [HttpPost("loginToStore")]
        public Response<bool> loginStore([FromBody] storeLoginRequest request)
        {
            return Service.Store.Instance.Login(request.accountNumber, request.storeId, request.email);
        }
        
    }
}