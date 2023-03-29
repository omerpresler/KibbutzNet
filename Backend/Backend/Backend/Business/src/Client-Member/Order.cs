namespace Backend.Business.Client_Member
{
    public class Order : Iorder
    {
        private int orderID { get; set; }
        
        public Order(int orderId)
        {
            orderID = orderId;
        }
    }
}