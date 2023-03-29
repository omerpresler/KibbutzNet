using Backend.Business.Client_Member;
using Backend.Business.src.Utils;
using Backend.Business.Utils;

namespace Backend.Service;

public class Member
{
    private ChatManager _chatManager;
    private OrderManager _orderManager;
    private User _user;
    private AuthenticationManager _authenticationManager;


    public Member(ChatManager chatManager, OrderManager orderManager, User user)
    {
        _chatManager = chatManager;
        _orderManager = orderManager;
        _user = user;
        _authenticationManager = AuthenticationManager.GetInstance();
    }

    public Response<string> login()
    {
        
    }
    
    

}