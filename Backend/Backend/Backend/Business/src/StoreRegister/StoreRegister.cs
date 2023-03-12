using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Backend.Business.src.Utils;
using Newtonsoft.Json;


namespace Backend.Business.src.StoreRegister
{
    public class StoreRegister
    {
        private int storeId { get; set; }
        private List<Purchase> purchases;
        private static int purchaseNum = 0;
        
        public StoreRegister(int storeId)
        {
            this.storeId = storeId;
            purchases = new List<Purchase>();
        }

        public bool addPurchase(int budgetNumber ,string description, float cost, int employeeId)
        {
            try
            {
                Purchase purchase = new Purchase(storeId, budgetNumber, Interlocked.Increment(ref purchaseNum), employeeId, cost, description);
                purchases.Add(purchase);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
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

        public ArrayList printPurchases()
        {
            return printPurchases(DateTime.MinValue, DateTime.MaxValue);
        }
        
        public ArrayList printPurchases(DateTime start)
        {
            return printPurchases(start, DateTime.MaxValue);
        }
        
        public ArrayList printPurchases(DateTime start, DateTime end)
        {
            ArrayList jsons = new ArrayList();
            foreach (Purchase p in purchases)
                if (DateTime.Compare(p.getDate(), start) > 0 && DateTime.Compare(p.getDate(), end) < 0)
                {
                    var purchase = new
                    {
                        PurchaseID = p.getPurchaseID(),
                        Date = p.getDate(),
                        BudgetNumber = p.getBudgetNumber(),
                        EmployeeID = p.getEmployeeID(),
                        Cost = p.getCost(),
                        Description = p.getDescription()
                    };
                    jsons.Add(JsonConvert.SerializeObject(purchase));
                }

            return jsons;
        }
        
        /*
         {
	        "PurchaseID" : Number,
	        "Date": "dd/mm/yyy",
	        "BudgetNumber" : Number,
	        "EmployeeID" : Number,
	        "Cost" : Number,
	        "Description" : "......",
        }
         */
        
        
        
    }
}