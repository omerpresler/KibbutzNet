namespace Backend.Controllers.Requests
{
    public class PurchaseHistoryRequest
    {
        
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int StoreId { get; set; }

        public PurchaseHistoryRequest(DateTime? start, DateTime? end, int storeId)
        {
            this.Start = start;
            this.End = end;
            this.StoreId = storeId;
        }
    }
}
