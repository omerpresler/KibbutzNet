namespace Backend.Controllers.Requests
{
    public class PurchaseHistoryRequest
    {
        

        public int StoreId { get; set; }
        public int UserId { get; set; }

        public PurchaseHistoryRequest(int storeId,int userId)
        {
            this.UserId = userId;
            this.StoreId = storeId;
        }
    }
}
