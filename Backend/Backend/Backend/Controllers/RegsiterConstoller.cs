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

        //float price string description int budget number int storeId int EmplooId 
        [HttpPost("OpenRegister")]
        public bool OpenRegister([FromBody] RegisterInfoRequest request)
        {

            bool res = Register.Instance.OpenRegister(request.StoreId, request.EmployeeId);
            Console.Write(res);
            return res;
        }

        //float Cost string description int budget number int storeId
        [HttpPost("addPurchase")]
        public Response<int> addPurchase([FromBody] AddPurchaseRequest request)
        {
            return Register.Instance.addPurchase(request.StoreId, request.BudgetNumber, request.Description, request.Cost);
        }

        //int StoreId 
        [HttpPost("SeePurchaseHistoryUser")]
        public ArrayList SeePurchaseHistoryUser([FromBody] PurchaseHistoryRequest request)
        {
            return Register.Instance.SeePurchaseHistory(request.StoreId);
        }

        //int StoreId 
        [HttpPost("SeePurchaseHistoryStore")]
        public ArrayList SeePurchaseHistoryStore([FromBody] PurchaseHistoryRequest request)
        {
            return Register.Instance.SeePurchaseHistory(request.StoreId);
        }

        [HttpPost("SeePurchaseHistoryUserAndStore")]
        public ArrayList SeePurchaseHistoryUserAndStore([FromBody] PurchaseHistoryRequest request)
        {
            return Register.Instance.SeePurchaseHistory(request.StoreId);
        }


        
        
    }
}