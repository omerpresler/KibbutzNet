using Backend.Access;
using NUnit.Framework;
using AdminAC = Backend.Access.Admin;
using AdminB = Backend.Business.src.Admin;
using AdminS = Backend.Service.Admin;
using Store = Backend.Service.Store;
using Newtonsoft.Json.Linq;

namespace Backend.Business.Test.StoreUnitTest
{
    [TestFixture]
    public class StoreUnitTest
    {
        private DBManager dbManager; 

        [SetUp]
        public void SetUp()
        {
            dbManager = DBManager.Instance;
        }
        
        [Test]
        public void AddNewStoreSuccess()
        {
            var adminService = AdminS.Instance;
            var store_id = adminService.CreateNewStore(0, "my-store", "").value;
            var stores = dbManager.LoadStores();
            Assert.IsTrue(stores.Exists(store => store.storeId == store_id));
        }
        
        [Test]
        public void AddNewStoreFail()
        {
            // admin doesn't exist
            var adminService = AdminS.Instance;
            var store_id = adminService.CreateNewStore(99, "my-store", "");
            Assert.IsTrue(store_id.exceptionHasOccured);
        }
        
        [Test]
        public void AssignEmployeeSuccess()
        {
            var adminService = AdminS.Instance;
            var store_id = adminService.CreateNewStore(0, "my-store", "").value;
            adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            adminService.AssignEmployeeToStore(0, 9999, store_id);
            var employees = dbManager.LoadStoreEmployees();
            Assert.IsTrue(employees.Exists(e => e.UserId == 9999 && e.storeId == store_id));
        }
        
        [Test]
        public void AssignEmployeeFail()
        {
            // admin doesn't exist
            var adminService = AdminS.Instance;
            var store_id = adminService.CreateNewStore(0, "my-store", "").value;
            adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var employee = adminService.AssignEmployeeToStore(8888, 9999, store_id);
            Assert.IsTrue(employee.exceptionHasOccured);
        }
        
        [Test]
        public void AssignEmployeeFail2()
        {
            // user doesn't exist
            var adminService = AdminS.Instance;
            var store_id = adminService.CreateNewStore(0, "my-store", "").value;
            var employee = adminService.AssignEmployeeToStore(0, 8888, store_id);
            Assert.IsTrue(employee.exceptionHasOccured);
        }
        
        [Test]
        public void AssignEmployeeFail3()
        {
            // store doesn't exist
            var adminService = AdminS.Instance;
            adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var employee = adminService.AssignEmployeeToStore(0, 9999, 99999);
            Assert.IsTrue(employee.exceptionHasOccured);
        }
        
        [Test]
        public void LoginSuccess()
        {
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my-store", "").value;
            adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            adminService.AssignEmployeeToStore(0, 9999, store_id);
            var login = storeService.Login(9999, store_id, "user@gmail.com");
            Assert.IsTrue(login.value);
        }
        
        [Test]
        public void LoginFail()
        {
            // the user is not an employee
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my-store", "").value;
            adminService.CreateNewMember(0, 9998, "user", "05444", "user1@gmail.com");
            var login = storeService.Login(9998, store_id, "user1@gmail.com");
            Assert.IsTrue(login.exceptionHasOccured);
        }
        
        [Test]
        public void LoginFail2()
        {
            // the email doesn't match to the userID
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my-store", "").value;
            adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            adminService.AssignEmployeeToStore(0, 9999, store_id);
            var login = storeService.Login(9999, store_id, "user1@gmail.com");
            Assert.IsTrue(login.exceptionHasOccured);
        }
        
        [Test]
        public void openChatSuccess()
        {
            // test only chat openning
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my store", "").value;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var openChat = storeService.OpenChat(store_id, 9999);
            var chats = dbManager.LoadChats();
            Assert.IsTrue(chats.Exists(chat => chat.store == store_id && chat.user == 9999));
        }
        
        [Test]
        public void openChatFail()
        {
            // store doesn't exist
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var openChat = storeService.OpenChat(99998, 9999);
            Assert.IsTrue(openChat.exceptionHasOccured);
        }
        
        [Test]
        public void openChatFail2()
        {
            // user doesn't exist
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my store", "").value;
            var openChat = storeService.OpenChat(store_id, 8888);
            Assert.IsTrue(openChat.exceptionHasOccured);
        }
        
        [Test]
        public void SendMessageSuccess()
        {
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my store", "").value;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var openChat = storeService.OpenChat(store_id, 9999);
            var chats = dbManager.LoadChats();
            var chat = chats.Find(c => c.user == 9999 && c.store == store_id).sessionId;
            var sendMessage = storeService.SendMessage(store_id, chat, "Hello World!");
            var messages = dbManager.LoadMessages();
            Assert.IsTrue(messages.Exists(message => message.chat == chat && message.message == "Hello World!"));
        }
        
        [Test]
        public void SendMessageFail()
        {
            // the chat doesn't exist
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my store", "").value;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var sendMessage = storeService.SendMessage(store_id, 9898, "Hello World!");
            Assert.IsTrue(sendMessage.exceptionHasOccured);
        }
        
        [Test]
        public void addOrderSuccess()
        {
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my store", "").value;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var order_id = storeService.addOrder(store_id, 9999, "user", "product", 500).value;
            var orders = dbManager.LoadOrders();
            Assert.IsTrue(orders.Exists(order => order.orderID == order_id && order.active));
        }
        
        [Test]
        public void changeStatusSuccess()
        {
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my store", "").value;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var order_id = storeService.addOrder(store_id, 9999, "user", "product", 500).value;
            storeService.changeOrdersStatus(store_id, order_id, "~accepted~");
            var orders = dbManager.LoadOrders();
            var status = orders.Find(o => o.orderID == order_id).status;
            Assert.AreEqual(status, "~accepted~");
        }
        
        [Test]
        public void changeStatusFail()
        {
            // the order doesn't exist
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my store", "").value;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var status = storeService.changeOrdersStatus(store_id, 5555, "~accepted~");
            Assert.IsTrue(status.exceptionHasOccured);
        }
        
        [Test]
        public void CloseOrderSuccess()
        {
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my store", "").value;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var order_id = storeService.addOrder(store_id, 9999, "user", "product", 500).value;
            storeService.closeOrder(store_id, order_id);
            var orders = dbManager.LoadOrders();
            Assert.IsTrue(orders.Exists(order => order.orderID == order_id && !order.active));
        }
        
        [Test]
        public void CloseAndReOpenOrderSuccess()
        {
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my store", "").value;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var order_id = storeService.addOrder(store_id, 9999, "user", "product", 500).value;
            storeService.closeOrder(store_id, order_id);
            var orders = dbManager.LoadOrders();
            Assert.IsTrue(orders.Exists(order => order.orderID == order_id && !order.active));
            storeService.reOpenOrder(store_id, order_id);
            orders = dbManager.LoadOrders();
            Assert.IsTrue(orders.Exists(order => order.orderID == order_id && order.active));
        }
        
        [Test]
        public void checkAllOrdersAreThere()
        {
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my store", "").value;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var order1 = storeService.addOrder(store_id, 9999, "user", "product", 500).value;
            var order2 = storeService.addOrder(store_id, 9999, "user", "product2", 40).value;
            var orders = dbManager.LoadOrders();
            var store_orders = orders.Where(o => o.storeId == store_id).ToList();
            var order_IDs = store_orders.Select(o => o.orderID).ToList();
            Assert.IsTrue(order_IDs.OrderBy(x => x).SequenceEqual(new List<int> {order1 , order2 }.OrderBy(x => x)));
        }
        
        [Test]
        public void OrderHistorySuccess()
        {
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my store", "").value;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            // add two orders
            var order1 = storeService.addOrder(store_id, 9999, "user", "product", 500).value;
            var order2 = storeService.addOrder(store_id, 9999, "user", "product2", 40).value;
            var orders = dbManager.LoadOrders();
            // get the orders fron DB
            var store_orders = orders.Where(o => o.storeId == store_id).Select(o => o.orderID.ToString()).ToList();
            // get the orders from charHistory
            var orderHistory = storeService.SeeOrderHistoryStore(store_id).value;
            // change both store_orders and orderHistory ID's to be of the same types (List<string>)
            var orderHistoryIDs = new List<string>();
            foreach (string o in orderHistory)
            {
                var jsonObject = JObject.Parse(o);
                foreach (var item in jsonObject)
                {
                    if (item.Key == "orderId")
                    {
                        orderHistoryIDs.Add(item.Value.ToString());
                    }
                }
            }
            // compare the two lists
            Assert.IsTrue(orderHistoryIDs.OrderBy(x => x).SequenceEqual(store_orders.OrderBy(x => x)));
        }

        [Test]
        public void AddPostSuccess()
        {
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my-store", "").value;
            var post = storeService.AddPost(store_id, "");
            Assert.IsFalse(post.exceptionHasOccured);
        }
        
        [Test]
        public void AddPostFail()
        {
            // store doesn't exist
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var post = storeService.AddPost(8989, "");
            Assert.IsTrue(post.exceptionHasOccured);
        }
        
        [Test]
        public void RemovePostSuccess()
        {
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my-store", "").value;
            var post_id = storeService.AddPost(store_id, "").value.Postid;
            var remove = storeService.RemovePost(store_id, post_id);
            Assert.IsFalse(remove.exceptionHasOccured);
        }

    }
}