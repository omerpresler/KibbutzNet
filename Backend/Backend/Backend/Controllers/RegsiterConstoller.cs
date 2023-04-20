using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Backend.Controllers.Requests;
using Backend.Service;
using Backend.Business.src.Utils;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        public RegisterController()
        {
        }

        //float price string description int budget number int storeId int EmployeeId 
        [HttpPost("OpenRegister")]
        public Response<string> OpenRegister([FromBody] RegisterInfoRequest request)
        {
            return Register.Instance.OpenRegister(request.StoreId, request.EmployeeId);
        }
        [HttpPost("CloseRegister")]
        public Response<bool> CloseRegister(int storeId)
        {
            return Register.Instance.CloseRegister(storeId);
        }

        //float Cost string description int budget number int storeId
        [HttpPost("addPurchase")]
        public Response<int> addPurchase([FromBody] AddPurchaseRequest request)
        {
            return Register.Instance.addPurchase(request.StoreId, request.BudgetNumber, request.Description, request.Cost);
        }

        //int StoreId 
        [HttpPost("SeePurchaseHistoryUser")]
        public Response<ArrayList> SeePurchaseHistoryUser([FromBody] PurchaseHistoryRequest request)
        {
            return Register.Instance.SeePurchaseHistoryUser(request.UserId);
        }

        //int StoreId 
        [HttpPost("SeePurchaseHistoryStore")]
        public Response<ArrayList> SeePurchaseHistoryStore([FromBody] PurchaseHistoryRequest request)
        {
            return Register.Instance.SeePurchaseHistoryStore(request.StoreId);
        }



        [HttpPost("SeePurchaseHistoryUserAndStore")]
        public Response<ArrayList> SeePurchaseHistoryUserAndStore([FromBody] PurchaseHistoryRequest request)
        {
            return Register.Instance.SeePurchaseHistoryUserAndStore(request.StoreId, request.UserId);
        }

    }
}