using Backend.Business.Utils;

namespace Backend.Business.src.Utils
{
    public class Outsider
    {
        private int userId;
        private int storeId;
        private string name;
        private string phoneNumber;

        public Outsider(int userId, int storeId, string name, string phoneNumber)
        {
            this.userId = userId;
            this.storeId = storeId;
            this.name = name;
            this.phoneNumber = phoneNumber;
        }
        
    }
}