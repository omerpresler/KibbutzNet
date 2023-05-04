using System.Security.Cryptography.Xml;
using Npgsql;

namespace Backend.Access;

public class Order
{
    public int orderID;
    public DateTime date;
    public string status { get; set; }
    public string memberName { get; set; }
    public int memberId { get; set; }
    public bool active { get; set; }
    public int chatId { get; set; }
    public float cost;
    public string description;
}