using System.Data;
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
        
        //CreateOrderTable();
        //CreatePurchaseTable();
        //CreateStoreTable();
    }
    
    private void CreateOrderTable()
    {
        ExecuteCommandNonQuery("DROP TABLE IF EXISTS Orders");
        ExecuteCommandNonQuery( @"CREATE TABLE Orders (
	                        orderId INT,
	                        storeId INT,
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
        ExecuteCommandNonQuery("DROP TABLE IF EXISTS Purchases");
        ExecuteCommandNonQuery( @"CREATE TABLE Purchases (
                            purchaseId INT,
	                        memberId INT,
	                        storeId INT,
	                        cost FLOAT,
	                        description VARCHAR(255),
	                        date DATE);" );
    }
    
    private void CreateStoreTable()
    {
        ExecuteCommandNonQuery("DROP TABLE IF EXISTS Stores");
        ExecuteCommandNonQuery( @"CREATE TABLE Stores (
                            storeId INT,
	                        storeName VARCHAR(255));" );
        
        string commandText = $@"insert into Stores (storeId, storeName) values (0, 'Flower Shop')";
        ExecuteCommandNonQuery(commandText);

    }
    
    public List<Store> LoadStores()
    {
        List<Store> stores = new List<Store>();
        
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = "localhost";
        sb.Port = 5432;
        sb.Username = "postgres";
        sb.Password = "omer";
        sb.Database = "KibbutzNet";

        List<string> rows = new List<string>();

        using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
        {
            conn.Open();

            // Define a query
            NpgsqlCommand command = new NpgsqlCommand("SELECT storeId, storeName FROM Stores", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                stores.Add(new Store(reader.GetInt32(0), reader.GetString(1)));
            }
            
            conn.Close();
        }
        return stores;
    }

    public List<Order> LoadOrders()
    {
        List<Order> orders = new List<Order>();
        
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = "localhost";
        sb.Port = 5432;
        sb.Username = "postgres";
        sb.Password = "omer";
        sb.Database = "KibbutzNet";

        List<string> rows = new List<string>();

        using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
        {
            conn.Open();

            // Define a query
            NpgsqlCommand command = new NpgsqlCommand("SELECT orderId, storeId, date, status, memberName, memberId, active, chatId, cost, description FROM Orders", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                int orderId = reader.GetInt32(0);
                int storeId = reader.GetInt32(1);
                int dateAsInt = reader.GetInt32(2);
                DateTime orderDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(dateAsInt).ToLocalTime();
                string status = reader.GetString(3);
                string memberName = reader.GetString(4);
                int memberId = reader.GetInt32(5);
                bool active = reader.GetBoolean(6);
                int chatId = reader.GetInt32(7);
                float cost = reader.GetFloat(8);
                string description = reader.GetString(9);

                orders.Add(new Order(orderId, storeId, orderDate, cost, description, status, memberName, memberId, active, chatId));
            }
            
            conn.Close();
        }
        return orders;
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

    public void AddOrder(Order order)
    {
        string commandText = $@"insert into Orders (orderId, storeId, date, status, memberName, memberId, active, chatId, cost, description) values ({order.orderID}, {order.storeId},'{order.date.Date}', '{order.status}', '{order.memberName}', {order.memberId}, {order.active}, {order.chatId}, {order.cost}, '{order.description}')";
        Console.WriteLine(commandText);
        ExecuteCommandNonQuery(commandText);
    }


    public void ExecuteCommandNonQuery(string commandText)
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
    
    public NpgsqlDataReader ExecuteCommandReader(string commandText)
    {
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = "localhost";
        sb.Port = 5432;
        sb.Username = "postgres";
        sb.Password = "omer";
        sb.Database = "KibbutzNet";

        List<string> rows = new List<string>();

        using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
        {
            conn.Open();

            // Define a query
            NpgsqlCommand command = new NpgsqlCommand(commandText, conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            
            return dr;
        }
        
    }
}