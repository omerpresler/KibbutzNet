namespace Backend.Access;

public class Member
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string email { get; set; }
    public List<int> houseHistoryList { get; set; }
    public int CurrHouse { get; set; }

    public Member(int userId, string name, string phoneNumber, string email, List<int> houseHistoryList, int currHouse)
    {
        UserId = userId;
        Name = name;
        PhoneNumber = phoneNumber;
        this.email = email;
        this.houseHistoryList = houseHistoryList;
        CurrHouse = currHouse;
    }
}