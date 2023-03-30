namespace Backend.Controllers.Requests
{
    public class PurchaseHistoryRequest
    {
        

        public int StoreId { get; set; }
        public int userId { get; set; }

        public PurchaseHistoryRequest(int storeId,int UserId)
        {
            this.userId = userId;
            this.StoreId = storeId;
        }
    }
}
