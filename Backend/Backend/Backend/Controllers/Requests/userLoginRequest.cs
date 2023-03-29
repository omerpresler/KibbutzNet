namespace Backend.Controllers.Requests
{
    public class userLoginRequest
    {
        public string email { get; set; }
        public int accountNumber { get; set; }

        public userLoginRequest(string email, int accountNumber)
        {
            this.email = email;
            this.accountNumber = accountNumber;
        }
    }
}
