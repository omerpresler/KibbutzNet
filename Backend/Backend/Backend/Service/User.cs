using Backend.Business.MemberController;
using Backend.Business.src.Utils;
using Backend.Business.Utils;
using Backend.Controllers;

namespace Backend.Service;

public class User
{
    private static Dictionary<int, MemberController> users;
    
    private static User instance;
    private static readonly object padlock = new object();

    private User()
    {
        users = new Dictionary<int, MemberController>();
    }

    public static User Instance {
        get {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new User();
                }
                return instance;
            }
        }
    }
    
    public void LoadMembers()
    {
        foreach (Access.Member DALMember in Access.DBManager.Instance.LoadMembers())
        {
            users.Add(DALMember.UserId, new MemberController(DALMember));
            AuthenticationManager.Instance.AddUser(DALMember.UserId, DALMember.email);
        }
    }


    public Response<bool> Login(int userId, string email)
    {
        try
        {
            if(!users.ContainsKey(userId))
                users.Add(userId, new MemberController(userId, email));
            else
                AuthenticationManager.Instance.Login(userId, email);
            
        }
        catch (Exception e)
        {
            return new Response<bool>(true, e.Message);
        }


        return new Response<bool>(true);
    }
    
    public Response<List<String>> GetAllchats(int userId)
    {
        try
        {
            if (users.ContainsKey(userId))
            {
                return users[userId].GetAllchats();
            }

            return new  Response<List<String>>(true, $"The is no user with the id of {userId}");
        }
        catch (Exception e)
        {
            return new  Response<List<String>>(true, e.Message);
        }
    }
}