using Backend.Business.Utils;

namespace Backend.Business.src.Utils
{
    public class Outsider : User
    {
        private int StoreId;
        public Outsider(int userId,int storeId,string name,string phoneNumber, string email) : base(userId, name, phoneNumber, email)
        {
            this.StoreId = storeId;
        }

    }
}