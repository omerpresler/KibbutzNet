using System.Security.Cryptography.Xml;
using Npgsql;

namespace Backend.Access;

public class Order
{
    public int orderID { get; set; }
    public int storeId { get; set; }
    public DateTime date { get; set; }
    public string status { get; set; }
    public string memberName { get; set; }
    public int memberId { get; set; }
    public bool active { get; set; }
    public int chatId { get; set; }
    public float cost { get; set; }
    public string description { get; set; }
    
    public Order(int orderId, int storeId, DateTime date, float cost, string description, string status, string memberName, int memberId, bool active, int chatId)
    {
        orderID = orderId;
        this.storeId = storeId;
        this.date = date;
        this.cost = cost;
        this.description = description;
        this.status = status;
        this.memberName = memberName;
        this.memberId = memberId;
        this.active = active;
        this.chatId = chatId;
    }
}