using Backend.Business.MemberController;
using Backend.Business.src.Utils;
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
                    
                    //Init
                    MemberController init = new MemberController(0, "emai@gmail.com");
                    users.Add(0, init);
                }
                return instance;
            }
        }
    }


    public Response<bool> Login(int userId, string email)
    {
        try
        {
            if(users.ContainsKey(userId))
                return new Response<bool>(true, $"User {userId} is logged in");
            users.Add(userId, new MemberController(userId, email));
        }
        catch (Exception e)
        {
            return new Response<bool>(true, e.Message);
        }


        return new Response<bool>(true);
    }
}