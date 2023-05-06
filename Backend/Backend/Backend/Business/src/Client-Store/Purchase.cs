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
        
        public Purchase(int storeId, int memberId, float cost, string desc)
        {
            this.storeId = storeId;
            this.memberId = memberId;
            this.purchaseId = Interlocked.Increment(ref nextPurchaseId);
            this.cost = cost;
            this.description = desc;
            this.date = DateTime.Now;
        }
        
    }
}