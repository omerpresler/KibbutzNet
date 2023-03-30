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
        public bool addPurchase([FromBody] AddPurchaseRequest request)
        {

            bool res = Register.Instance.addPurchase(request.StoreId, request.BudgetNumber, request.Description,
                request.Cost);
            ;
            Console.Write(res);
            return res;
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
            // if(request.Start == null)
            return Register.Instance.SeePurchaseHistory(request.StoreId);
                // if (request.End == null)
                //   return Register.Instance.SeePurchaseHistory(request.StoreId, request.Start??DateTime.Now);

                // return Register.Instance.SeePurchaseHistory(request.StoreId, request.Start??DateTime.Now, request.End??DateTime.Now);
        }
    }
}