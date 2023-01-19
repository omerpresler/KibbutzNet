using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Business.src.Utils;

namespace Backend.Business.src.StoreRegister
{
    public class StoreRegister
    {
        private int storeId { get; set; }
        //private int employeeId { get; set; }
        private List<Purchase> purchases;
        private int purchaseNum;
        
        public StoreRegister(int storeId)
        {
            this.storeId = storeId;
            //this.employeeId = employeeId;
            purchases = new List<Purchase>();
            purchaseNum = 0;
        }

        public bool addPurchase(int budgetNumber ,string description, float cost, int employeeId)
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

        public void printPurchases()
        {
            if (purchases.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Date\t\t\tID   Budget   Employee  Cost  Description");
                Console.WriteLine("________________________________________________________________");
                foreach (var p in purchases)
                {
                    Console.WriteLine(p.getDate()+"\t"+p.getPurchaseID()+"\t"+p.getBudgetNumber()+"\t"+p.getEmployeeID()+"\t"+p.getCost()+"\t"+p.getDescription());
                }
                Console.WriteLine();
                return;
            }

            Console.WriteLine("No purchases to display.");
        }
        
    }
}