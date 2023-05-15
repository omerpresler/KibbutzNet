using System;

namespace Backend.Business.src.Utils
{
    public class Purchase
    {
        public int memberId { get; set; }
        public int storeId { get; set; }
        private static int nextPurchaseId = 0;
        public int purchaseId{ get; set; }
        public float cost { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        
        public Purchase(int memberId, int storeId, int purchaseId, float cost, string description, DateTime date)
        {
            this.memberId = memberId;
            this.storeId = storeId;
            this.purchaseId = purchaseId;
            this.cost = cost;
            this.description = description;
            this.date = date;
        }
    }
}