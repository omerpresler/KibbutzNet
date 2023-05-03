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
        
        public Response<int> OpenChat(int storeId)
        {
            return cm.StartChat(storeId, member.UserId);
        }

        public Response<string> SendMessage(int sessionId, string msg)
        {
            return cm.SendMessage(sessionId, new Message<string>(member.UserId, msg));
        }

        public Response<List<String>> GetAllchats()
        {
            return cm.GetAllUserChats(member.UserId);
        }
    }
}