namespace Controllers.Requests
{
    public class UserDataRequest
    {
        public string email { get; set; }
        public int acountNum { get; set; }
        public UserDataRequest(string email, int acountNum)
        {
            this.email = email;
            this.acountNum = acountNum;
        }
    }
}
