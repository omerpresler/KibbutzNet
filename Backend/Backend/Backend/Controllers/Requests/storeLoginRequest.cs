namespace Backend.Controllers.Requests
{
    public class storeLoginRequest
    {
            public string email { get; set; }
            public string accountNumber { get; set; }
            public string storeId { get; set; }

            public storeLoginRequest(string email, string accountNumber,string storeID)
            {
                this.email = email;
                this.accountNumber = accountNumber;
                this.storeId = storeID;
            }
        }

    }
