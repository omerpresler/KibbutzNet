namespace Backend.Controllers.Requests
{
    public class PurchaseHistoryRequest
    {
        

        public int StoreId { get; set; }
        public int UserId { get; set; }

        public PurchaseHistoryRequest(int StoreId,int UserId)
        {
            this.UserId = UserId;
            this.StoreId = StoreId;
        }
    }
}
