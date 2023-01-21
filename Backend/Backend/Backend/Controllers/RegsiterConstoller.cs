using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Controllers.Requests;
using Backend.Service;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        public RegisterController() { }
        
        //float price string description int budget number int storeId int EmplooId 
        [HttpPost("OpenRegister")]
        public bool OpenRegister([FromBody] RegisterInfoRequest request)
        {
            Register.Instance.OpenRegister(request.StoreId, request.EmployeeId);
            return true;
        }
        
        //float Cost string description int budget number int storeId
        [HttpPost("addPurchase")]
        public bool addPurchase([FromBody] AddPurchaseRequest request)
        {
            Register.Instance.addPurchase(request.StoreId, request.BudgetNumber, request.Description, request.Cost);

            return true;
        }
        
        //int StoreId 
        [HttpPost("SeePurchaseHistory")]
        public string SeePurchaseHistory([FromBody] PurchaseHistoryRequest request)
        {
            return Register.Instance.SeePurchaseHistory(request.StoreId);
        }
        //register -add new purchse see purchse history
        //store-client-get report 




    }
}