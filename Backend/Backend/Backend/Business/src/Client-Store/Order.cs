using System;
using Backend.Business.src.Utils;

namespace Backend.Business.src.Client_Store
{
    public class Order
    {
        public virtual int orderID { get; }
        public virtual DateTime date { get; }
        public virtual string status { get; set; }
        public virtual string memberName { get; set; }
        public virtual int memberId { get; set; }
        private bool active { get; set; }
        private Chat Chat { get; set; }
        public virtual float cost { get; set; }
        public virtual string description { get; set; }

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