namespace Backend.Controllers.Requests
{
    public class purchaseHistoryRequest
    {

        public string from { get; set; }
        public string to { get; set; }

        public purchaseHistoryRequest(string from, string to)
        {
            this.from = from;
            this.to= to;
        }
    }
}
