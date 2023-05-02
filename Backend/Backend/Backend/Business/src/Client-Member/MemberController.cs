using Backend.Business.src.Utils;
using Backend.Business.Utils;

namespace Backend.Business.MemberController
{
    public class MemberController
    {
        private Member member;
        private ChatManager cm;
        
        
        public MemberController(int userId, string email)
        {
            member = AuthenticationManager.Instance.Login(userId, email);
            cm = new ChatManager();
        }
    }
}