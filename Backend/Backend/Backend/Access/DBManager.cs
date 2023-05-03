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

    private async Task CreateTables()
    {
        var con = new NpgsqlConnection(
            connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=password;Database=KibbutzNet;");
        con.Open();
        using var cmd = new NpgsqlCommand();
        cmd.Connection = con;

        cmd.CommandText = "DROP TABLE IF EXISTS Orders";
        await cmd.ExecuteNonQueryAsync();
        cmd.CommandText = @"CREATE TABLE `Orders` (
	                        orderId INT,
	                        date DATETIME,
	                        status VARCHAR(255),
	                        memberName VARCHAR,
	                        memberId INT,
	                        active BOOLEAN,
	                        chatId INT,
	                        cost INT,
	                        description INT);";
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
}