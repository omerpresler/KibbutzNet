namespace Backend.Controllers.Requests
{
    public class storeLoginRequest
    {
            public string email { get; set; }
            public int accountNumber { get; set; }
            public int storeId { get; set; }

            public storeLoginRequest(string email, int accountNumber, int storeId)
            {
                this.email = email;
                this.accountNumber = accountNumber;
                this.storeId = storeId;
            }
        }

    }
