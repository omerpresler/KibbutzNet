namespace Backend.Controllers.Requests
{
    public class UserDataRequest
    {
        public string email { get; set; }
        public string accountNumber { get; set; }
        public UserDataRequest(string email, string accountNumber)
        {
            this.email = email;
            this.accountNumber = accountNumber;
        }
    }
}
