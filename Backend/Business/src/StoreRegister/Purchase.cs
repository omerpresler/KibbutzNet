using System;

namespace Backend.Business.src.StoreRegister
{
    public class Purchase
    {
        private int budgetNumber { get; set; }
        private int storeID { get; set; }
        private int purchaseID { get; set; }
        private int employeeID { get; set; }
        private float amount { get; set; }
        private string description { get; set; }
        private DateTime date { get; set; }
        
        public Purchase(int store, int budgetNumber ,int purchase, int employee, float amount, string desc)
        {
            storeID = store;
            this.budgetNumber = budgetNumber;
            purchaseID = purchase;
            employeeID = employee;
            this.amount = amount;
            description = desc;
            date = DateTime.Now;
        }
        
        public int getBudgetNumber()
        {
            return budgetNumber;
        }

        public int getPurchaseID()
        {
            return purchaseID;
        }

        public DateTime getDate()
        {
            return date;
        }
    }
}