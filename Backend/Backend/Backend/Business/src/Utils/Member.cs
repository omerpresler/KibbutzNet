using System.Collections.Generic;

namespace Backend.Business.Utils
{
    public class Member : User
    {
        private List<House> houseHistoryList;
        private House CurrHouse;
        
        public Member(int userId, string name,string phoneNumber, string email) : base(userId, name, phoneNumber, email)
        {
            this.houseHistoryList = new List<House>();
            this.CurrHouse = new House(-1, "TODO: pass parameter");
        }

    }
}