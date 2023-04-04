using System;

namespace Backend.Business.src.StoreRegister
{
    public class Purchase
    {
        public int budgetNumber { get; set; }
        private int storeID { get; set; }
        public int purchaseID { get; set; }
        private int employeeID { get; set; }
        private float cost { get; set; }
        private string description { get; set; }
        private DateTime date { get; set; }
        
        public Purchase(int store, int budgetNumber ,int purchase, int employee, float cost, string desc)
        {
            storeID = store;
            this.budgetNumber = budgetNumber;
            purchaseID = purchase;
            employeeID = employee;
            this.cost = cost;
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

        public float getCost()
        {
            return cost;
        }

        public string getDescription()
        {
            return description;
        }

        public int getEmployeeID()
        {
            return employeeID;
        }
    }
}