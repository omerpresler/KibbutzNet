namespace Backend.Business.Client_Member
{
    public class Purchase : iPurchase
    {
        private int storeID;
        private int purchaseID;
        private int employeeID;
        private float cost;
        private string description;
        private DateTime date;

        public Purchase(int storeId, int purchaseId, int employeeId, float cost, string description, DateTime date)
        {
            storeID = storeId;
            purchaseID = purchaseId;
            employeeID = employeeId;
            this.cost = cost;
            this.description = description;
            this.date = date;
        }

        public int getPurchaseID()
        {
            return purchaseID;
        }

        public int getStoreID()
        {
            return storeID;
        }

        public float getCost()
        {
            return cost;
        }

        public DateTime getDate()
        {
            return date;
        }
        
    }
}