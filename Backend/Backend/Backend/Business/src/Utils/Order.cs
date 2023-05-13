using System;
using Backend.Business.src.Utils;

namespace Backend.Business.src.Utils
{
    public class Order
    {
        public int orderId { get; set; }
        public DateTime date { get; set; }
        public string status { get; set; }
        public string memberName { get; set; }
        public int memberId { get; set; }
        public bool active { get; set; }
        public float cost { get; set; }
        public string description { get; set; }

        public Order(int orderId, string memberName, int memberId, bool active, float cost, string description)
        {
            this.orderId = orderId;
            date = DateTime.Now;
            status = "";
            this.memberId = memberId;
            this.memberName = memberName;
            this.active = active;
            this.cost = cost;
            this.description = description;
        }
        
        public Order(Access.Order order)
        {
            orderId = order.orderID;
            date = order.date;
            status = order.status;
            memberId = order.memberId;
            memberName = order.memberName;
            active = order.active;
            cost = order.cost;
            description = order.description;
        }

        public override string ToString()
        {
            return $"{memberName}\t{memberId}\t{cost}\t{description}\t{status}";
        }
        
        public Backend.Access.Order ToDalObject(int storeId)
        {
            //TODO implement chat id
            return new Backend.Access.Order(orderId, storeId, date, cost, description, status, memberName, memberId, active, 0);
        }

    }
}