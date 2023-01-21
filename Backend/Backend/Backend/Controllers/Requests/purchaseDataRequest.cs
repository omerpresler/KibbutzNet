namespace Backend.Controllers.Requests
{
    public class purchaseDataRequest
    {

        public string price { get; set; }
        public string description { get; set; }
        public string accountNumber { get; set; }

        public purchaseDataRequest(string price, string description, string accountNumber)
        {
            this.price = price;
            this.description = description; 
            this.accountNumber = accountNumber;
        }
    }
}
