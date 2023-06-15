using Backend.Access;
using Backend.Business.src.Utils;
using NUnit.Framework;
using AdminAC = Backend.Access.Admin;
using AdminB = Backend.Business.src.Admin;
using AdminS = Backend.Service.Admin;
using User = Backend.Service.User;
using Store = Backend.Service.Store;
using Newtonsoft.Json.Linq;
using Message = Backend.Access.Message;


namespace Backend.Business.Test.clientTest
{
    [TestFixture]
    public class clientUnitTests
    {
        
        [SetUp]
        public void SetUp()
        {
            DBManager.Instance.wipeDB();
            DBManager.Instance.initBasicData();
        }

        [TearDown]
        public void TearDown()
        {
        }
        
        [Test]
        public void AddNewMemberSuccess()
        {
            int userId = 9999;
            
            Response<bool> createNewMemberResponse = AdminS.Instance.CreateNewMember(0, userId, "user", "05444", "user@gmail.com");
            Assert.IsFalse(createNewMemberResponse.exceptionHasOccured);
            List<Member> members = DBManager.Instance.LoadMembers();
            Assert.IsTrue(members.Exists(member => member.UserId == userId));
        }
        
        [Test]
        public void AddNewMemberFail_SameIdTwice()
        {
            int userId = 8888;
            Response<bool> createNewMemberResponse = AdminS.Instance.CreateNewMember(0, userId, "user", "05444", "user@gmail.com");
            Assert.IsFalse(createNewMemberResponse.exceptionHasOccured);
            Response<bool> createAnotherMemberResponse = AdminS.Instance.CreateNewMember(0, userId, "user", "05444", "user@gmail.com");
            Assert.IsTrue(createAnotherMemberResponse.exceptionHasOccured);
        }
        
        [Test]
        public void AddNewMemberFail_NoSuchAdmin()
        {
            int userId = 7777;
            Response<bool> createNewMemberResponse = AdminS.Instance.CreateNewMember(1, userId, "user", "05444", "user@gmail.com");
            Assert.IsTrue(createNewMemberResponse.exceptionHasOccured);
        }
        
        [Test]
        public void LoginSuccess()
        {
            int userId = 6666;
            Response<bool> createNewMemberResponse = AdminS.Instance.CreateNewMember(0, userId, "user", "05444", "user@gmail.com");
            Assert.IsFalse(createNewMemberResponse.exceptionHasOccured);
            Response<string> loginResponse = User.Instance.Login(userId, "user@gmail.com");
            Assert.IsFalse(loginResponse.exceptionHasOccured);
            Assert.AreEqual(loginResponse.value, "user");
        }
        
        [Test]
        public void LoginFail_NonMatchingEmail()
        {
            int userId = 5555;
            Response<bool> createNewMemberResponse = AdminS.Instance.CreateNewMember(0, userId, "user", "05444", "user@gmail.com");
            Assert.IsFalse(createNewMemberResponse.exceptionHasOccured);
            Response<string> loginResponse = User.Instance.Login(userId, "not-a-user@gmail.com");
            Assert.IsTrue(loginResponse.exceptionHasOccured);
        }

        [Test]
        public void SendMessageSuccess()
        {
            int userId = 4444;
            Response<bool> createNewMemberResponse = AdminS.Instance.CreateNewMember(0, userId, "user", "05444", "user@gmail.com");
            Response<int> createNewStoreResponse = AdminS.Instance.CreateNewStore(0, "my store", "");
            
            Assert.IsFalse(createNewMemberResponse.exceptionHasOccured);
            Assert.IsFalse(createNewStoreResponse.exceptionHasOccured);
            
            Response<string> sendMessageResponse = User.Instance.SendMessage(userId, createNewStoreResponse.value, "Hello World!");
            Assert.IsFalse(sendMessageResponse.exceptionHasOccured);
            
            List<Message> messages = DBManager.Instance.LoadMessagesPerChat(userId, createNewStoreResponse.value);
            Assert.IsTrue(messages.Exists(message => message.message == "Hello World!")); 
        }
        
        [Test]
        public void SendMessageFail_NoSuchStore()
        {
            int userId = 3333;
            Response<bool> createNewMemberResponse = AdminS.Instance.CreateNewMember(0, userId, "user", "05444", "user@gmail.com");
            Response<int> createNewStoreResponse = AdminS.Instance.CreateNewStore(0, "my store", "");
            
            Assert.IsFalse(createNewMemberResponse.exceptionHasOccured);
            Assert.IsFalse(createNewStoreResponse.exceptionHasOccured);
            
            Response<string> sendMessage = User.Instance.SendMessage(userId, -1, "Hello World!");
            Assert.IsTrue(sendMessage.exceptionHasOccured);
        }
        
        [Test]
        public void SendMessageFail_NoSuchUser()
        {
            int userId = 2222;
            Response<bool> createNewMemberResponse = AdminS.Instance.CreateNewMember(0, userId, "user", "05444", "user@gmail.com");
            Response<int> createNewStoreResponse = AdminS.Instance.CreateNewStore(0, "my store", "");
            
            Assert.IsFalse(createNewMemberResponse.exceptionHasOccured);
            Assert.IsFalse(createNewStoreResponse.exceptionHasOccured);
            
            Response<string> sendMessage = User.Instance.SendMessage(-1, 88888, "Hello World!");
            Assert.IsTrue(sendMessage.exceptionHasOccured);
        }
    }
}