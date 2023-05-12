namespace Backend.Controllers.Requests;

public class createNewMemberRequest
{
    public int adminId {get; set;}
    public int userId {get; set;}
    public string name {get; set;}
    public string phoneNumber {get; set;}
    public string email {get; set;}
    
    public createNewMemberRequest(int adminId, int userId, string name, string phoneNumber, string email)
    {
        this.adminId = adminId;
        this.userId = userId;
        this.name = name;
        this.phoneNumber = phoneNumber;
        this.email = email;
    }

}