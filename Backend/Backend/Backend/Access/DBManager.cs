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
        CreateOrderTable();
        CreatePurchaseTable();
        CreateStoreTable();
        CreateMemberTable();
        CreateAdminTable();
        CreateStoreEmployeeTable();

        initBasicData();
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
    }
    
    private void CreateMemberTable()
    {
        ExecuteCommandNonQuery("DROP TABLE IF EXISTS Members");
        ExecuteCommandNonQuery( @"CREATE TABLE Members (
                            userId INT,
	                        memberName VARCHAR(255),
	                        phoneNumber VARCHAR(255),
	                        email VARCHAR(255),
	                        house INT);" );
    }
    
    private void CreateAdminTable()
    {
        ExecuteCommandNonQuery("DROP TABLE IF EXISTS Admins");
        ExecuteCommandNonQuery( @"CREATE TABLE Admins (
                            userId INT,
	                        email VARCHAR(255));" );
    }
    
    private void CreateStoreEmployeeTable()
    {
        ExecuteCommandNonQuery("DROP TABLE IF EXISTS StoreEmployees");
        ExecuteCommandNonQuery( @"CREATE TABLE StoreEmployees (
                            userId INT,
	                        storeId INT);" );
    }

    private void initBasicData()
    {
        string commandText = $@"insert into Stores (storeId, storeName) values (0, 'Flower Shop')";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into Members (userId, memberName, phoneNumber, email, house) values (0, 'amit', '054-444-4444', 'amit@gmail.com', 1)";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into Members (userId, memberName, phoneNumber, email, house) values (1, 'omer', '054-333-3333', 'omer@gmail.com', 2)";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into Admins (userId, email) values (0, 'admin@gmail.com')";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into StoreEmployees (userId, storeId) values (0, 0)";
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
    
    public List<Member> LoadMembers()
    {
        List<Member> members = new List<Member>();
        
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
            NpgsqlCommand command = new NpgsqlCommand("SELECT userId, memberName, phoneNumber, email, house FROM Members", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                int userId = reader.GetInt32(0);
                string memberName = reader.GetString(1);
                string phoneNumber = reader.GetString(2);
                string email = reader.GetString(3);
                int house = reader.GetInt32(4);

                members.Add(new Member(userId, memberName, phoneNumber, email, new List<int>(), house));
            }
            
            conn.Close();
        }
        return members;
    }
    
    public List<Admin> LoadAdmins()
    {
        List<Admin> admins = new List<Admin>();
        
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
            NpgsqlCommand command = new NpgsqlCommand("SELECT userId, email FROM Admins", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                int userId = reader.GetInt32(0);
                string email = reader.GetString(1);

                admins.Add(new Admin(userId, email));
            }
            
            conn.Close();
        }
        return admins;
    }
    
    public List<StoreEmployee> LoadStoreEmployees()
    {
        List<StoreEmployee> StoreEmployees = new List<StoreEmployee>();
        
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
            NpgsqlCommand command = new NpgsqlCommand("SELECT userId, storeId FROM StoreEmployees", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                int userId = reader.GetInt32(0);
                int storeId = reader.GetInt32(1);

                StoreEmployees.Add(new StoreEmployee(userId, storeId));
            }
            
            conn.Close();
        }
        return StoreEmployees;
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
        string commandText = $@"insert into Members (orderId, storeId, date, status, memberName, memberId, active, chatId, cost, description) values ({order.orderID}, {order.storeId},'{order.date.Date}', '{order.status}', '{order.memberName}', {order.memberId}, {order.active}, {order.chatId}, {order.cost}, '{order.description}')";
        ExecuteCommandNonQuery(commandText);
    }
    
    public void AddStore(int storeId, string storeName)
    {
        string commandText = $@"insert into Stores (storeId, storeName) values ({storeId}, '{storeName}')";
        ExecuteCommandNonQuery(commandText);
    }
    
    public void AddMember(int userId, string name,string phoneNumber, string email)
    {
        string commandText = $@"insert into Members (userId, memberName, phoneNumber, email, house) values ({userId}, '{name}', '{phoneNumber}', '{email}', {0})";
        ExecuteCommandNonQuery(commandText);
    }
    
    public void AddStoreEmployees(StoreEmployee storeEmployee)
    {
        string commandText = $@"insert into StoreEmployees (userId, storeId) values ({storeEmployee.UserId}, {storeEmployee.storeId})";
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