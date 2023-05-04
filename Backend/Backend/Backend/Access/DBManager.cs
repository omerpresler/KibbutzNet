using Npgsql;

namespace Backend.Access;

public class DBManager
{
    
    private static DBManager instance;
    private static readonly object padlock = new object();
    private NpgsqlCommand cmd;

    public DBManager()
    {
        CreateTables();
    }

    private async void CreateTables()
    {
        var con = new NpgsqlConnection(
            connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=omer;Database=KibbutzNet;");
        con.Open();
        cmd = new NpgsqlCommand();
        cmd.Connection = con;

        CreateOrderTable();
        CreatePurchaseTable();
    }
    
    private async void CreateOrderTable()
    {
        cmd.CommandText = "DROP TABLE IF EXISTS Orders";
        await cmd.ExecuteNonQueryAsync();
        cmd.CommandText = @"CREATE TABLE Orders (
	                        orderId INT,
	                        date DATE,
	                        status VARCHAR(255),
	                        memberName VARCHAR,
	                        memberId INT,
	                        active BOOLEAN,
	                        chatId INT,
	                        cost INT,
	                        description INT);";
        await cmd.ExecuteNonQueryAsync();
    }
    
    private async void CreatePurchaseTable()
    {
        cmd.CommandText = "DROP TABLE IF EXISTS Orders";
        await cmd.ExecuteNonQueryAsync();
        cmd.CommandText = @"CREATE TABLE Orders (
                            purchaseId INT,
	                        memberId INT,
	                        storeId INT,
	                        cost FLOAT,
	                        description VARCHAR(255),
	                        date DATE);";
        await cmd.ExecuteNonQueryAsync();
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


    public void printTest()
    {
        Console.WriteLine("Test");
    }
}