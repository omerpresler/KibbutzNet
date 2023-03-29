using Backend.Business.src.Utils;

namespace Backend.Business.Client_Member
{
    public class PurchaseManager : IPurchaseManger
    {
        private List<Purchase> _purchases;

        public PurchaseManager(List<Purchase> purchases)
        {
            _purchases = purchases;
        }

        public Response<string> addPurchase()
        {
            return new Response<string>("purchase was added.");
        }


    }
}