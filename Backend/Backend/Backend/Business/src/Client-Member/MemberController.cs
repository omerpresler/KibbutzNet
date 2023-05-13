using Backend.Business.src.Utils;
using Backend.Business.Utils;

namespace Backend.Business.MemberController
{
    public class MemberController
    {
        private Member member;
        private ChatManager cm;


        public MemberController(int userId, string name,string phoneNumber, string email)
        {
            member = new Member(userId, name, phoneNumber, email);
            cm = new ChatManager(userId, false);
        }
        
        public MemberController(Backend.Access.Member DALMember)
        {
            member = new Member(DALMember);
            cm = new ChatManager(DALMember.UserId, false);
        }

        public int getMemberId()
        {
            return member.UserId;
        }
        
        public string getMemberEmail()
        {
            return member.email;
        }
        
        public Response<int> OpenChat(int storeId)
        {
            return cm.StartChat(storeId, member.UserId);
        }

        public Response<string> SendMessage(int sessionId, string msg)
        {
            return cm.SendMessage(sessionId, new Message<string>(false, msg), false);
        }

        public Response<List<String>> GetAllchats()
        {
            return cm.GetAllUserChats(member.UserId);
        }
    }
}