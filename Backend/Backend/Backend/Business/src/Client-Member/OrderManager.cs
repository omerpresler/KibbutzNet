using Backend.Business.src.Utils;

namespace Backend.Business.Client_Member
{
    public class OrderManager
    {
        private List<Order> _orders;
        private int orderNum = 0;


        public OrderManager()
        {
            _orders = new List<Order>();
        }

        public Response<string> addOrder()
        {
            return new Response<string>("order was added");
        }

        public Response<string> removeOrder()
        {
            return new Response<string>("order was removed");
        }

        public Response<string> changeStatus()
        {
            return new Response<string>("status was changed");
        }

    }
}