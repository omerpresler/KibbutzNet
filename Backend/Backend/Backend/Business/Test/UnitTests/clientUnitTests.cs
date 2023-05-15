using Backend.Access;
using NUnit.Framework;
using AdminAC = Backend.Access.Admin;
using AdminB = Backend.Business.src.Admin;
using AdminS = Backend.Service.Admin;
using User = Backend.Service.User;
using Store = Backend.Service.Store;
using Newtonsoft.Json.Linq;



namespace Backend.Business.Test.clientTest
{
    [TestFixture]
    public class clientUnitTests
    {
        private DBManager dbManager; 

        [SetUp]
        public void SetUp()
        {
            dbManager = DBManager.Instance;
        }

        [TearDown]
        public void TearDown()
        {
            
        }
        
        [Test]
        public void AddNewMemberSuccess()
        {
            //var adminAccess = new AdminAC(1, "admin1@gmail.com");
            //var adminBusiness = new AdminB(adminAccess);
            //adminBusiness.CreateNewMember(9999, "user", "05444", "user@gmail.com").getMemberId();
            var adminService = AdminS.Instance;
            adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var members = dbManager.LoadMembers();
            Assert.IsTrue(members.Exists(member => member.UserId == 9999));
            
        }
        
        [Test]
        public void AddNewMemberFail()
        {
            // 2 users with same ID
            var adminService = AdminS.Instance;
            adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var member2 = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            Assert.IsTrue(member2.exceptionHasOccured);
        }
        
        [Test]
        public void AddNewMemberFail2()
        {
            // non-existing admin creates user
            var adminService = AdminS.Instance;
            var member = adminService.CreateNewMember(1, 9999, "user", "05444", "user@gmail.com");
            Assert.IsTrue(member.exceptionHasOccured);
        }
        
        [Test]
        public void LoginSuccess()
        {
            var adminService = AdminS.Instance;
            var userService = User.Instance;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var login = userService.Login(9999, "user@gmail.com");
            Assert.AreEqual(login.value, "user");
        }
        
        [Test]
        public void LoginFail()
        {
            // email doesn't match with userID
            var adminService = AdminS.Instance;
            var userService = User.Instance;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var login = userService.Login(9999, "not-a-user@gmail.com");
            Assert.IsTrue(login.exceptionHasOccured);
        }
        
        [Test]
        public void openChatSuccess()
        {
            // test only chat openning
            var adminService = AdminS.Instance;
            var userService = User.Instance;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var store_id = adminService.CreateNewStore(0, "my store", "").value;
            var openChat = userService.OpenChat(9999, store_id);
            var chats = dbManager.LoadChats();
            Assert.IsTrue(chats.Exists(chat => chat.store == store_id && chat.user == 9999));
        }
        
        [Test]
        public void openChatFail()
        {
            // no such store
            var adminService = AdminS.Instance;
            var userService = User.Instance;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var openChat = userService.OpenChat(9999, 99998);
            Assert.IsTrue(openChat.exceptionHasOccured);
        }
        
        [Test]
        public void openChatFail2()
        {
            // no such user
            var adminService = AdminS.Instance;
            var userService = User.Instance;
            var store_id = adminService.CreateNewStore(0, "my store", "").value;
            var openChat = userService.OpenChat(9998, store_id);
            var chats = dbManager.LoadChats();
            Assert.IsTrue(openChat.exceptionHasOccured);
        }
        
        [Test]
        public void SendMessageSuccess()
        {
            var adminService = AdminS.Instance;
            var userService = User.Instance;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var store_id = adminService.CreateNewStore(0, "my store", "").value;
            var openChat = userService.OpenChat(9999, store_id);
            var chats = dbManager.LoadChats();
            var chat = chats.Find(c => c.user == 9999 && c.store == store_id).sessionId;
            var sendMessage = userService.SendMessage(9999, chat, "Hello World!");
            var messages = dbManager.LoadMessages();
            Assert.IsTrue(messages.Exists(message => message.chat == chat && message.message == "Hello World!"));
        }
        
        [Test]
        public void SendMessageFail()
        {
            // no such chat
            var adminService = AdminS.Instance;
            var userService = User.Instance;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var store_id = adminService.CreateNewStore(0, "my store", "").value;
            var sendMessage = userService.SendMessage(9999, 88888, "Hello World!");
            Assert.IsTrue(sendMessage.exceptionHasOccured);
        }
        

    }
}