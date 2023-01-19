using System;
using Backend.Business.Utils;

namespace Backend.Business.src.Client_Store
{
    public class Order
    {
        public string status { get; set; }
        public string memberName { get; set; }
        public int memberId { get; set; }
        public bool active { get; set; }
        public Chat Chat { get; set; }

        public Order(string status, string memberName, string memverId, bool active)
        {
            this.status = status;
            this.memberId = memberId;
            this.memberName = memberName;
            this.memberId = memberId;
            this.active = active;
        }
    }
}