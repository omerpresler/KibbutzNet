namespace Backend.Business.src.Utils
{
    public class Outsider : User
    {
        public Outsider(int userId, int storeId, string name, string phoneNumber) : base(userId, storeId, name, phoneNumber){}
        
    }
}