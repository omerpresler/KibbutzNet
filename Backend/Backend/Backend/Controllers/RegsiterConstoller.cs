using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Backend.Controllers.Requests;
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
            return Register.Instance.addPurchase(request.StoreId, request.BudgetNumber, request.Description, request.Cost);;
        }
        
        //int StoreId 
        [HttpPost("SeePurchaseHistory")]
        public ArrayList SeePurchaseHistory([FromBody] PurchaseHistoryRequest request)
        {
//            if(request.Start == null)
                return Register.Instance.SeePurchaseHistory(request.StoreId);
//            if (request.End == null)
//                return Register.Instance.SeePurchaseHistory(request.StoreId, request.Start??DateTime.Now);
//
//            return Register.Instance.SeePurchaseHistory(request.StoreId, request.Start??DateTime.Now, request.End??DateTime.Now);
        }
        //register -add new purchse see purchse history
        //store-client-get report 




    }
}