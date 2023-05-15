using System.Data;
using System.Drawing.Drawing2D;
using Npgsql;

namespace Backend.Access;

public class DBManager
{
    
    private static DBManager instance;
    private static readonly object padlock = new object();
    
    private string Host = "localhost";
    private int Port = 5432;
    private string Username = "postgres";
    private string  Password = "omer";
    private string Database = "kibbutznet";

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
        CreateMessageTable();
        CreateChatTable();
        
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
	                        storeName VARCHAR(255),
	                        photoLink VARCHAR(255));" );
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
    
    private void CreateMessageTable()
    {
        ExecuteCommandNonQuery("DROP TABLE IF EXISTS Messages");
        ExecuteCommandNonQuery( @"CREATE TABLE Messages (
                            placeInChat INT,
                            chat INT,
                            fromStore BOOLEAN,
	                        message VARCHAR(255));" );
    }
    
    private void CreateChatTable()
    {
        ExecuteCommandNonQuery("DROP TABLE IF EXISTS Chats");
        ExecuteCommandNonQuery( @"CREATE TABLE Chats (
                            sessionId INT,
                            storeId INT,
                            userId INT,
                            active BOOLEAN,
                            startDate DATE);" );
    }

    private void initBasicData()
    {
        string commandText = $@"insert into Stores (storeId, storeName, photoLink) values (0, 'Flower Shop', 'https://images.pexels.com/photos/16619444/pexels-photo-16619444.jpeg?auto=compress&cs=tinysrgb&w=600&lazy=load')";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into Members (userId, memberName, phoneNumber, email, house) values (0, 'amit', '054-444-4444', 'amit@gmail.com', 1)";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into Members (userId, memberName, phoneNumber, email, house) values (1, 'omer', '054-333-3333', 'omer@gmail.com', 2)";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into Admins (userId, email) values (0, 'admin@gmail.com')";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into StoreEmployees (userId, storeId) values (0, 0)";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into Orders (orderId, storeId, date, status, memberName, memberId, active, chatId, cost, description) values (0, 0,'{DateTime.Now}', 'In the oven', 'Amit', 0, TRUE, 0, 15, 'a cake')";
        ExecuteCommandNonQuery(commandText);
    }
    
    public List<Store> LoadStores()
    {
        List<Store> stores = new List<Store>();
        
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;

        List<string> rows = new List<string>();

        using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
        {
            conn.Open();

            // Define a query
            NpgsqlCommand command = new NpgsqlCommand("SELECT storeId, storeName, photoLink FROM Stores", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                stores.Add(new Store(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
            }
            
            conn.Close();
        }
        return stores;
    }

    public List<Order> LoadOrders()
    {
        List<Order> orders = new List<Order>();
        
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;

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
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;

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
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;

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
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;

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
    
    public List<Message> LoadMessages()
    {
        List<Message> Messages = new List<Message>();
        
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;

        List<string> rows = new List<string>();

        using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
        {
            conn.Open();

            // Define a query
            NpgsqlCommand command = new NpgsqlCommand("SELECT placeInChat, chat, fromStore, message FROM Messages", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                int placeInChat = reader.GetInt32(0);
                int chat = reader.GetInt32(1);
                bool fromStore = reader.GetBoolean(2);
                string message = reader.GetString(3);

                Messages.Add(new Message(placeInChat, chat, fromStore, message));
            }
            
            conn.Close();
        }
        return Messages;
    }
    
    public List<Message> LoadMessagesPerChat(int chatId)
    {
        List<Message> Messages = new List<Message>();
        
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;

        List<string> rows = new List<string>();

        using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
        {
            conn.Open();

            // Define a query
            NpgsqlCommand command = new NpgsqlCommand($"SELECT placeInChat, chat, fromStore, message FROM Messages WHERE chat = {chatId}", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                int placeInChat = reader.GetInt32(0);
                int chat = reader.GetInt32(1);
                bool fromStore = reader.GetBoolean(2);
                string message = reader.GetString(3);

                Messages.Add(new Message(placeInChat, chat, fromStore, message));
            }
            
            conn.Close();
        }
        return Messages;
    }
    
    public List<Chat> LoadChats()
    {
        List<Chat> Chats = new List<Chat>();
        
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;

        List<string> rows = new List<string>();

        using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
        {
            conn.Open();

            // Define a query
            NpgsqlCommand command = new NpgsqlCommand("SELECT sessionId, storeId, userId, active, startDate FROM Chats", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                int sessionId = reader.GetInt32(0); 
                int store = reader.GetInt32(1); 
                int user = reader.GetInt32(2); 
                bool active = reader.GetBoolean(3); 
                int dateAsInt = reader.GetInt32(4);
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(dateAsInt).ToLocalTime();

                Chats.Add(new Chat(sessionId, store, user, active, start));
            }
            
            conn.Close();
        }
        return Chats;
    }
    
    public List<Chat> LoadChatsPerUser(int userId)
    {
        List<Chat> Chats = new List<Chat>();
        
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;

        List<string> rows = new List<string>();

        using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
        {
            conn.Open();

            // Define a query
            NpgsqlCommand command = new NpgsqlCommand($"SELECT sessionId, storeId, userId, active, startDate FROM Chats WHERE userId = {userId}", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                int sessionId = reader.GetInt32(0); 
                int store = reader.GetInt32(1); 
                int user = reader.GetInt32(2); 
                bool active = reader.GetBoolean(3);
                int dateAsInt = reader.GetInt32(4);
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(dateAsInt).ToLocalTime();

                Chats.Add(new Chat(sessionId, store, user, active, start));
            }
            
            conn.Close();
        }
        return Chats;
    }
    
    public List<Chat> LoadChatsPerStore(int storeId)
    {
        List<Chat> Chats = new List<Chat>();
        
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;

        List<string> rows = new List<string>();

        using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
        {
            conn.Open();

            // Define a query
            NpgsqlCommand command = new NpgsqlCommand($"SELECT sessionId, storeId, userId, active, startDate FROM Chats WHERE storeId = {storeId}", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                int sessionId = reader.GetInt32(0); 
                int store = reader.GetInt32(1); 
                int user = reader.GetInt32(2); 
                bool active = reader.GetBoolean(3); 
                int dateAsInt = reader.GetInt32(4);
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(dateAsInt).ToLocalTime();

                Chats.Add(new Chat(sessionId, store, user, active, start));
            }
            
            conn.Close();
        }
        return Chats;
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
        ExecuteCommandNonQuery(commandText);
    }
    
    public void AddStore(int storeId, string storeName, string photoLink)
    {
        string commandText = $@"insert into Stores (storeId, storeName, photoLink) values ({storeId}, '{storeName}', '{photoLink}')";
        ExecuteCommandNonQuery(commandText);
    }
    
    public void AddMember(int userId, string name,string phoneNumber, string email)
    {
        string commandText = $@"insert into Members (userId, memberName, phoneNumber, email, house) values ({userId}, '{name}', '{phoneNumber}', '{email}', {0})";
        ExecuteCommandNonQuery(commandText);
    }
    
    public void AddStoreEmployee(StoreEmployee storeEmployee)
    {
        string commandText = $@"insert into StoreEmployees (userId, storeId) values ({storeEmployee.UserId}, {storeEmployee.storeId})";
        ExecuteCommandNonQuery(commandText);
    }
    
    public void AddMessage(int placeInChat, int chat, bool fromStore, string message)
    {
        string commandText = $@"insert into Messages (placeInChat, chat, fromStore, message) values ({placeInChat}, {chat}, {fromStore}, '{message}')";
        ExecuteCommandNonQuery(commandText);
    }
    
    public void AddChat(int sessionId, int storeId, int userId, bool active, DateTime startDate)
    {
        string commandText = $@"insert into Chats (sessionId, storeId, userId, active, startDate) values ({sessionId}, {storeId}, {userId}, {active}, '{startDate}')";
        ExecuteCommandNonQuery(commandText);
    }

    public void updateOrderActiveField(int orderId, bool active)
    {
        string commandText = $@"UPDATE Orders SET active={active} WHERE orderId={orderId};";
        ExecuteCommandNonQuery(commandText);
    }
    
    public void updateOrderStatusField(int orderId, string status)
    {
        string commandText = $@"UPDATE Orders SET status='{status}' WHERE orderId={orderId};";
        ExecuteCommandNonQuery(commandText);
    }
    
    public void updateChatActiveField(int sessionId, bool active)
    {
        string commandText = $@"UPDATE chats SET active={active} WHERE sessionId={sessionId};";
        ExecuteCommandNonQuery(commandText);
    }

    public int getMaxStoreId()
    {
        List<Store> stores = new List<Store>();
        int maxStoreId = 0;
        
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;

        List<string> rows = new List<string>();

        using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
        {
            conn.Open();

            // Define a query
            NpgsqlCommand command = new NpgsqlCommand($"SELECT MAX(storeId) FROM Stores;", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                maxStoreId = reader.GetInt32(0);
            }
            
            conn.Close();
        }
        
        return maxStoreId;
    }
    
    public int getMaxOrderId()
    {
        List<Store> stores = new List<Store>();
        int maxOrderId = 0;
        
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;

        List<string> rows = new List<string>();

        using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
        {
            conn.Open();

            // Define a query
            NpgsqlCommand command = new NpgsqlCommand($"SELECT MAX(orderId) FROM Orders;", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                maxOrderId = reader.GetInt32(0);
            }
            
            conn.Close();
        }
        
        return maxOrderId;
    }
    
    public int getMaxChatId()
    {
        try
        {
            int maxOrderId = 0;
        
            NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
            sb.Host = Host;
            sb.Port = Port;
            sb.Username = Username;
            sb.Password = Password;
            sb.Database = Database;

            List<string> rows = new List<string>();

            using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
            {
                conn.Open();

                // Define a query
                NpgsqlCommand command = new NpgsqlCommand($"SELECT MAX(sessionId) FROM Chats;", conn);

                // Execute the query and obtain a result set
                NpgsqlDataReader reader = command.ExecuteReader();
            
                while (reader.Read())
                {
                    maxOrderId = reader.GetInt32(0);
                }
            
                conn.Close();
            }
        
            return maxOrderId;
        }
        catch (Exception e)
        {
            return 0;
        }
        
    }


    public void ExecuteCommandNonQuery(string commandText)
    {
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;
        
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