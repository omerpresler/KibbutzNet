using System;
using Backend.Business.src.Utils;

namespace Backend.Business.src.Client_Store
{
    public class Order
    {
        private int orderID;
        private DateTime date;
        private string status { get; set; }
        private string memberName { get; set; }
        private int memberId { get; set; }
        private bool active { get; set; }
        private Chat Chat { get; set; }
        private float cost;
        private string description;

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