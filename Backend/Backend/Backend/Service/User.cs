using Backend.Business.MemberController;
using Backend.Business.src.Utils;
using Backend.Business.Utils;
using Backend.Controllers;

namespace Backend.Service;

public class User
{
    private static Dictionary<int, MemberController> users = new Dictionary<int, MemberController>();
    
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
    
    public void cleanUser()
    {
        lock (padlock)
            instance = new User();
    }
    
    public bool userExist(int userId)
    {
        return users.ContainsKey(userId);
    }
    
    public string getUserName(int userId)
    {
        return users[userId].getMemberName();
    }
    
    public void addNewMember(MemberController member)
    {
        users.Add(member.getMemberId(), member);
    }
    
    public void LoadMembers()
    {
        foreach (Access.Member DALMember in Access.DBManager.Instance.LoadMembers())
        {
            users.Add(DALMember.UserId, new MemberController(DALMember));
            AuthenticationManager.Instance.AddUser(DALMember.UserId, DALMember.email);
        }
    }


    public Response<string> Login(int userId, string email)
    {
        try
        {
            AuthenticationManager.Instance.Login(userId, email);
        }
        catch (Exception e)
        {
            return new Response<string>(true, e.Message);
        }


        return new Response<string>("user");
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

    public Response<string> SendMessage(int userId, int storeId, string msg)
    {
        try
        {
            if (!users.ContainsKey(userId))
                return new Response<string>(true, $"The is no user with the id of {userId}");
            
            if(!Store.Instance.storeExist(storeId))
                return new Response<string>(true, $"The is no store with the id of {storeId}");
            
            return users[userId].SendMessage(storeId, msg);
            

            
        }
        catch (Exception e)
        {
            return new Response<string>(true, e.Message);
        }
    }
}