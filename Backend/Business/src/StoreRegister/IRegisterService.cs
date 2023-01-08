using System;
using Backend.Business.src.Utils;

namespace Backend.Business.src.StoreRegister
{
    public interface IRegisterService
    {
        void addNewRegister(int storeID, int emplyeeID);
        bool addPurchase(int storeId, int budgetNumber ,string description, float amount);
        bool getPurchaseByBudgetNumber(int storeId, int budget);
        bool getPurchaseByDate(int storeId, DateTime from, DateTime until);
        bool removePurchase(int storeId, int purchaseNum);
    }
}

