namespace Backend.Business.Client_Member
{
    public class Purchase : iPurchase
    {
        public virtual int purchaseID { get; set; }

        public Purchase(int purchaseId)
        {
            purchaseID = purchaseId;
        }
    }
}