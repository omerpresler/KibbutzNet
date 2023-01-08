using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Business.src.StoreRegister
{
    public class Service : IRegisterService
    {
        private List<StoreRegister> registers;
        
        public Service()
        {
            registers = new List<StoreRegister>();
        }

        public void addNewRegister(int storeID, int emplyeeID)
        {
            var register = new StoreRegister(storeID, emplyeeID);
            registers.Add(register);
        }
        
        private StoreRegister getStoreRegByStoreID(int storeID)
        {
            StoreRegister register = null;
            foreach (var reg in registers.Where(reg => reg.getStoreID() == storeID))
            {
                register = reg;
            }
            return register;
        }

        public bool addPurchase(int storeId, int budgetNumber, string description, float amount)
        {
            var register = getStoreRegByStoreID(storeId);
            return register != null && register.addPurchase(budgetNumber, description, amount);
        }

        public bool getPurchaseByBudgetNumber(int storeId, int budget)
        {
            var register = getStoreRegByStoreID(storeId);
            return register != null && register.getPurchaseByBudgetNumber(budget) != null;
        }

        public bool getPurchaseByDate(int storeId, DateTime from, DateTime until)
        {
            var register = getStoreRegByStoreID(storeId);
            return register != null && register.getPurchaseByDate(from, until) != null;
        }

        public bool removePurchase(int storeId, int purchaseNum)
        {
            var register = getStoreRegByStoreID(storeId);
            return register != null && register.removePurchase(purchaseNum);
        }
        
    }
}