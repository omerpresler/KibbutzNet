namespace Backend.Access;

public class Admin
{
    public int UserId { get; set; }
    public string email { get; set; }
    
    public Admin(int userId, string email)
    {
        UserId = userId;
        this.email = email;
    }
}