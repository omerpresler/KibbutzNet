using System;
using Backend.Business.src.Utils;

namespace Backend.Business.src.Utils
{
    public class Order
    {
        public int orderID;
        public DateTime date;
        public string status { get; set; }
        public string memberName { get; set; }
        public int memberId { get; set; }
        public bool active { get; set; }
        public Chat Chat { get; set; }
        public float cost;
        public string description;

        public Order(int orderId, string memberName, int memberId, bool active, float cost, string description)
        {
            orderID = orderId;
            date = DateTime.Now;
            status = "received";
            this.memberId = memberId;
            this.memberName = memberName;
            this.active = active;
            this.cost = cost;
            this.description = description;
        }

        public override string ToString()
        {
            return $"{memberName}\t{memberId}\t{cost}\t{description}\t{status}";
        }

        public int getID()
        {
            return orderID;
        }

        public void setStatus(string status)
        {
            this.status = status;
        }

    }
}