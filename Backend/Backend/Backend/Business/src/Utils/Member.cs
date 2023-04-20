using System.Collections.Generic;

namespace Backend.Business.Utils
{
    public class Member : User
    {
        public virtual List<House> houseHistoryList { get; set; }
        public virtual House CurrHouse { get; set; }
        
        public Member(int userId,int storeId,string name,string phoneNumber) : base(userId, storeId, name, phoneNumber)
        {
            this.houseHistoryList = new List<House>();
            this.CurrHouse = new House(-1, "TODO: pass parameter");
        }

    }
}