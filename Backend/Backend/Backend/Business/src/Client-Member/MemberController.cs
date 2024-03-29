using System.Dynamic;
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
            cm = new ChatManager();
        }
        
        public MemberController(Backend.Access.Member DALMember)
        {
            member = new Member(DALMember);
            cm = new ChatManager();
        }

        public Member getMember()
        {
            return member;
        }

        public int getMemberId()
        {
            return member.UserId;
        }
        
        public string getMemberEmail()
        {
            return member.email;
        }
        
        public string getMemberName()
        {
            return member.Name;
        }

        public Response<string> SendMessage
            
            (int storeId, string msg)
        {
            return cm.SendMessage(member.UserId, storeId, new Message(false, msg), false);
        }

        public Response<List<String>> GetAllchats()
        {
            return cm.GetAllUserChats(member.UserId);
        }
    }
}