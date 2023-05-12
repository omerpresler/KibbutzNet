using Backend.Business.src.Utils;
using Microsoft.AspNetCore.Mvc;

using Backend.Controllers.Requests;
using Backend.Service;


namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        public AdminController()
        { }
        
        [HttpPost("assignEmployeeToStore")]
        public Response<bool> assignEmployeeToStore([FromBody] assignEmployeeToStoreRequest request)
        {
            return Admin.Instance.AssignEmployeeToStore(request.adminId, request.userId, request.storeId);
        }

        [HttpPost("createNewStore")]
        public Response<bool> createNewStore([FromBody] createNewStoreRequest request)
        {
            return Admin.Instance.CreateNewStore(request.adminId, request.storeId, request.storeName);
        }
        
        [HttpPost("createNewMember")]
        public Response<bool> createNewMember([FromBody] createNewMemberRequest request)
        {
            return Admin.Instance.CreateNewMember(request.adminId, request.userId, request.name, request.phoneNumber, request.email);
        }
    }
}