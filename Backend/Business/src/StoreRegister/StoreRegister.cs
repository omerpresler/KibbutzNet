using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Business.src.Utils;

namespace Backend.Business.src.StoreRegister
{
    public class StoreRegister
    {
        private int storeId { get; set; }
        private int employeeId { get; set; }
        private List<Purchase> purchases;
        private int purchaseNum;
        
        public StoreRegister(int storeId, int employeeId)
        {
            this.storeId = storeId;
            this.employeeId = employeeId;
            purchases = new List<Purchase>();
            purchaseNum = 0;
        }

        public bool addPurchase(int budgetNumber ,string description, float cost)
        {
            purchaseNum += 1;
            var purchase = new Purchase(storeId,budgetNumber, purchaseNum, employeeId, cost, description);
            purchases.Add(purchase);
            return true;
        }

        public bool removePurchase(int purchaseNum)
        {
            foreach (var p in purchases.Where(p => p.getPurchaseID() == purchaseNum))
            {
                purchases.Remove(p);
                return true;
            }
            return false;
        }

        public Purchase getPurchaseByBudgetNumber(int budget)
        {
            return purchases.FirstOrDefault(p => p.getBudgetNumber() == budget);
        }

        public Purchase getPurchaseByID(int purchaseID)
        {
            return purchases.FirstOrDefault(p => p.getPurchaseID() == purchaseID);
        }

        public List<Purchase> getPurchaseByDate(DateTime from, DateTime until)
        {
            var purch = new List<Purchase>();
            foreach (var p in purchases)
            {
                if (DateTime.Compare(from.Date, p.getDate().Date) <= 0 &&
                    DateTime.Compare(p.getDate().Date, until.Date) <= 0)
                {
                    purch.Add(p);
                }
            }
            return purch;
        }

        public int getStoreID()
        {
            return storeId;
        }
        
    }
}