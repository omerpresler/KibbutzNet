using System.Drawing.Drawing2D;
using Npgsql;

namespace Backend.Access;

public class DBManager
{
    
    private static DBManager instance;
    private static readonly object padlock = new object();

    public DBManager()
    {
        CreateTables();
    }

    private void CreateTables()
    {
        
        CreateOrderTable();
        CreatePurchaseTable();
    }
    
    private void CreateOrderTable()
    {
        ExecuteCommand("DROP TABLE IF EXISTS Orders");
        ExecuteCommand( @"CREATE TABLE Orders (
	                        orderId INT,
	                        date DATE,
	                        status VARCHAR(255),
	                        memberName VARCHAR(255),
	                        memberId INT,
	                        active BOOLEAN,
	                        chatId INT,
	                        cost INT,
	                        description VARCHAR(255));" );
    }
    
    private void CreatePurchaseTable()
    {
        ExecuteCommand("DROP TABLE IF EXISTS Purchases");
        ExecuteCommand( @"CREATE TABLE Purchases (
                            purchaseId INT,
	                        memberId INT,
	                        storeId INT,
	                        cost FLOAT,
	                        description VARCHAR(255),
	                        date DATE);" );
    }
    
        
    public static DBManager Instance {
        get {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DBManager();
                }
                return instance;
            }
        }
    }

    public void AddProduct(Order order)
    {
        string commandText = $@"insert into Orders (orderId, date, status, memberName, memberId, active, chatId, cost, description) values ({order.orderID}, '{order.date.Date}', '{order.status}', '{order.memberName}', {order.memberId}, {order.active}, {order.chatId}, {order.cost}, '{order.description}')";
        Console.WriteLine(commandText);
        ExecuteCommand(commandText);
    }


    public void ExecuteCommand(string commandText)
    {
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = "localhost";
        sb.Port = 5432;
        sb.Username = "postgres";
        sb.Password = "omer";
        sb.Database = "KibbutzNet";
        
        using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
        {
            conn.Open();
            using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, conn))
                cmd.ExecuteNonQuery();
            
        }
    }
}