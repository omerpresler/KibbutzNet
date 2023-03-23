namespace Backend.Controllers.Requests
{
    public class PurchaseHistoryRequest
    {
        
        public String  Start { get; set; }
        public String End { get; set; }
        public int StoreId { get; set; }

        public PurchaseHistoryRequest(String start, String end, int storeId)
        {
            this.Start = start;
            this.End = end;
            this.StoreId = storeId;
        }
    }
}
