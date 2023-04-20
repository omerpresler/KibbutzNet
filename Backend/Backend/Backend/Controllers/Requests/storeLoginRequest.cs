namespace Backend.Controllers.Requests
{
    public class storeLoginRequest
    {
            public string password { get; set; }
            public int accountNumber { get; set; }
            public int storeId { get; set; }

            public storeLoginRequest(string password, int accountNumber, int storeId)
            {
                this.password = password;
                this.accountNumber = accountNumber;
                this.storeId = storeId;
            }
        }

    }
