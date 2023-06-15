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
    private string Database = "KibbutzNet";

    public DBManager()
    {
        wipeDB();
        initBasicData();
    }

    public void wipeDB()
    {
        CreateOrderTable();
        CreatePurchaseTable();
        CreateStoreTable();
        CreateMemberTable();
        CreateAdminTable();
        CreateStoreEmployeeTable();
        CreateMessageTable();
        CreateChatTable();
        CreatePostTable();
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
                            userId INT,
                            storeId INT,
                            fromStore BOOLEAN,
	                        message VARCHAR(255));" );
    }
    
    private void CreateChatTable()
    {
        ExecuteCommandNonQuery("DROP TABLE IF EXISTS Chats");
        ExecuteCommandNonQuery( @"CREATE TABLE Chats (
                            storeId INT,
                            userId INT,
                            active BOOLEAN,
                            startDate DATE);" );
    }
    
    private void CreatePostTable()
    {
        ExecuteCommandNonQuery("DROP TABLE IF EXISTS Posts");
        ExecuteCommandNonQuery( @"CREATE TABLE Posts (
                            postId INT,
                            storeId INT,
                            header VARCHAR(255),
                            photoLink VARCHAR(255));" );
    }

    public void initBasicData()
    {
        string commandText = $@"insert into Stores (storeId, storeName, photoLink) values (0, 'Flower Shop', 'https://cdn.pixabay.com/photo/2023/04/29/09/43/bee-7958148_960_720.jpg')";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into Members (userId, memberName, phoneNumber, email, house) values (0, 'amit', '054-444-4444', 'amit@gmail.com', 1)";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into Members (userId, memberName, phoneNumber, email, house) values (1, 'omer', '054-333-3333', 'omer@gmail.com', 2)";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into Admins (userId, email) values (0, 'admin@gmail.com')";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into StoreEmployees (userId, storeId) values (0, 0)";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into Orders (orderId, storeId, date, status, memberName, memberId, active, chatId, cost, description) values (0, 0,'{DateTime.Now}', 'Still growing', 'Omer', 1, TRUE, 0, 11, '2x Roses')";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into Orders (orderId, storeId, date, status, memberName, memberId, active, chatId, cost, description) values (1, 0,'{DateTime.Now}', 'In the garden', 'Amit', 0, TRUE, 0, 13.2, 'A nice bouquet of yellow flowers')";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into Orders (orderId, storeId, date, status, memberName, memberId, active, chatId, cost, description) values (2, 0,'{DateTime.Now}', 'Ready for delivery', 'Rotem', 0, TRUE, 0, 14.3, 'A little cactus')";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into purchases (storeId,memberId ,purchaseId ,cost ,description ,date) values (0, 1, 0, 14.6, 'Some random ass cactus', '{DateTime.Now}')";
        ExecuteCommandNonQuery(commandText);
        
        commandText = $@"insert into purchases (storeId, memberId ,purchaseId ,cost ,description ,date) values (0, 1, 1, 12.3, 'God knows', '{DateTime.Now}')";
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

    public List<Message> LoadMessagesPerChat(int userId, int storeId)
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
            NpgsqlCommand command = new NpgsqlCommand($"SELECT placeInChat, userId, storeId, fromStore, message FROM Messages WHERE userId = {userId} AND storeId = {storeId}", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                int placeInChat = reader.GetInt32(0);
                bool fromStore = reader.GetBoolean(3);
                string message = reader.GetString(4);

                Messages.Add(new Message(placeInChat, userId, storeId, fromStore, message));
            }
            
            conn.Close();
        }
        return Messages;
    }
    
    public List<Post> LoadPostsPerStore(int storeId)
    {
        List<Post> posts = new List<Post>();
        
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
            NpgsqlCommand command = new NpgsqlCommand($"SELECT postId, storeId, header ,photoLink FROM Posts WHERE storeId = {storeId}", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                int postId = reader.GetInt32(0);
                string header = reader.GetString(2);
                string photoLink = reader.GetString(3);
                

                posts.Add(new Post(postId, storeId, header, photoLink));
            }
            
            conn.Close();
        }
        return posts;
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
            NpgsqlCommand command = new NpgsqlCommand("SELECT storeId, userId, active, startDate FROM Chats", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                int store = reader.GetInt32(0); 
                int user = reader.GetInt32(1); 
                bool active = reader.GetBoolean(2); 
                int dateAsInt = reader.GetInt32(3);
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(dateAsInt).ToLocalTime();

                Chats.Add(new Chat(store, user, active, start));
            }
            
            conn.Close();
        }
        return Chats;
    }
    
    public List<Purchase> LoadPurchasesPerStore(int storeId)
    {
        List<Purchase> purchases = new List<Purchase>();
        
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;
        
        using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
        {
            conn.Open();

            // Define a query
            NpgsqlCommand command = new NpgsqlCommand($"SELECT purchaseId ,memberId ,storeId ,cost ,description ,date FROM purchases WHERE storeId = {storeId}", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                int purchaseId = reader.GetInt32(0);
                int memberId = reader.GetInt32(1);
                double doubleValue = reader.GetDouble(3);
                float cost = Convert.ToSingle(doubleValue);
                string description = reader.GetString(4);
                int dateAsInt = reader.GetInt32(5);
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(dateAsInt).ToLocalTime();
                

                purchases.Add(new Purchase(storeId,memberId, purchaseId, cost, description, start));
            }
            
            conn.Close();
        }
        return purchases;
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
    
    public void AddMessage(int placeInChat, int userId, int storeId, bool fromStore, string message)
    {
        string commandText = $@"insert into Messages (placeInChat, userId, storeId, fromStore, message) values ({placeInChat}, {userId}, {storeId}, {fromStore}, '{message}')";
        ExecuteCommandNonQuery(commandText);
    }
    
    public void AddChat(int storeId, int userId, bool active, DateTime startDate)
    {
        string commandText = $@"insert into Chats (storeId, userId, active, startDate) values ({storeId}, {userId}, {active}, '{startDate}')";
        ExecuteCommandNonQuery(commandText);
    }
    
    public void AddPost(int postId, int storeId, String header, string photoLink)
    {
        string commandText = $@"insert into Posts (postId, storeId, header ,photoLink) values ({postId}, {storeId}, '{header}', '{photoLink}')";
        ExecuteCommandNonQuery(commandText);
    }
    
    public void AddPurchase(int storeId, int memberId, int purchaseId, float cost, string description, DateTime date)
    {
        string commandText = $@"insert into purchases ( storeId,memberId ,purchaseId ,cost ,description ,date) values ({storeId}, {memberId}, {purchaseId}, {cost}, '{description}', '{date}')";
        ExecuteCommandNonQuery(commandText);
    }

    public void RemovePost(int postId)
    {
        string commandText = $@"DELETE FROM Posts WHERE postId = {postId}";
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

    public int getMaxStoreId()
    {
        int maxStoreId = 0;
        
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;
        
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
    
    public int getMaxPostId()
    {
        int maxPostId = 0;
        
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;
        
        using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
        {
            conn.Open();

            // Define a query
            NpgsqlCommand command = new NpgsqlCommand($"SELECT MAX(postId) FROM Posts;", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                maxPostId = reader.GetInt32(0);
            }
            
            conn.Close();
        }
        
        return maxPostId;
    }
    
    public int getMaxPurchaseId()
    {
        int maxPurchaseId = 0;
        
        NpgsqlConnectionStringBuilder sb = new NpgsqlConnectionStringBuilder();
        sb.Host = Host;
        sb.Port = Port;
        sb.Username = Username;
        sb.Password = Password;
        sb.Database = Database;


        using (NpgsqlConnection conn = new NpgsqlConnection(sb.ToString()))
        {
            conn.Open();

            // Define a query
            NpgsqlCommand command = new NpgsqlCommand($"SELECT MAX(purchaseId) FROM purchases;", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                maxPurchaseId = reader.GetInt32(0);
            }
            
            conn.Close();
        }
        
        return maxPurchaseId;
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