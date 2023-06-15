using System.Runtime.InteropServices.JavaScript;
using Backend.Access;
using Backend.Business.src.Utils;
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
        [SetUp]
        public void SetUp()
        {
            DBManager.Instance.wipeDB();
            DBManager.Instance.initBasicData();
        }
        
        [Test]
        public void AddNewStoreSuccess()
        {
            Response<int> createNewStoreResponse = AdminS.Instance.CreateNewStore(0, "my-store", "");
            Assert.IsFalse(createNewStoreResponse.exceptionHasOccured);

            List<Access.Store> stores = DBManager.Instance.LoadStores();
            Assert.IsTrue(stores.Exists(store => store.storeId == createNewStoreResponse.value));
        }
        
        [Test]
        public void AddNewStoreFail()
        {
            Response<int> createNewStoreResponse = AdminS.Instance.CreateNewStore(99, "my-store", "");
            Assert.IsTrue(createNewStoreResponse.exceptionHasOccured);
        }
        
        [Test]
        public void AssignEmployeeSuccess()
        {
            int userId = 3840;
            
            Response<int> createNewStoreResponse = AdminS.Instance.CreateNewStore(0, "my-store", "");
            Assert.IsFalse(createNewStoreResponse.exceptionHasOccured);
           
            Response<bool> createNewMemberResponse = AdminS.Instance.CreateNewMember(0, userId, "user", "05444", "user@gmail.com");
            Assert.IsFalse(createNewMemberResponse.exceptionHasOccured);
            
            Response<bool> assignEmployeeToStoreResponse = AdminS.Instance.AssignEmployeeToStore(0, userId, createNewStoreResponse.value);
            Assert.IsFalse(assignEmployeeToStoreResponse.exceptionHasOccured);

            
            List<StoreEmployee> employees = DBManager.Instance.LoadStoreEmployees();
            Assert.IsTrue(employees.Exists(e => e.UserId == userId && e.storeId == createNewStoreResponse.value));
        }
        
        [Test]
        public void AssignEmployeeFail_NoSuchAdmin()
        {
            int userId = 5759;

            Response<int> createNewStoreResponse = AdminS.Instance.CreateNewStore(0, "my-store", "");
            Assert.IsFalse(createNewStoreResponse.exceptionHasOccured);
           
            Response<bool> createNewMemberResponse = AdminS.Instance.CreateNewMember(0, userId, "user", "05444", "user@gmail.com");
            Assert.IsFalse(createNewMemberResponse.exceptionHasOccured);
            
            Response<bool> assignEmployeeToStoreResponse = AdminS.Instance.AssignEmployeeToStore(-1, userId, createNewStoreResponse.value);
            Assert.IsTrue(assignEmployeeToStoreResponse.exceptionHasOccured);
        }
        
        [Test]
        public void AssignEmployeeFail_NoSuchUser()
        {
            Response<int> createNewStoreResponse = AdminS.Instance.CreateNewStore(0, "my-store", "");
            Assert.IsFalse(createNewStoreResponse.exceptionHasOccured);

            Response<bool> assignEmployeeToStoreResponse = AdminS.Instance.AssignEmployeeToStore(0, -1, createNewStoreResponse.value);
            Assert.IsTrue(assignEmployeeToStoreResponse.exceptionHasOccured);
        }
        
        [Test]
        public void AssignEmployeeFail_NoSuchStore()
        {
            int userId = 8284;

            Response<bool> createNewMemberResponse = AdminS.Instance.CreateNewMember(0, userId, "user", "05444", "user@gmail.com");
            Assert.IsFalse(createNewMemberResponse.exceptionHasOccured);
            
            Response<bool> assignEmployeeToStoreResponse = AdminS.Instance.AssignEmployeeToStore(0, userId, -1);
            Assert.IsTrue(assignEmployeeToStoreResponse.exceptionHasOccured);
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
        public void SendMessageSuccess()
        {
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my store", "").value;
            var member = adminService.CreateNewMember(0, 9999, "user", "05444", "user@gmail.com");
            var chats = DBManager.Instance.LoadChats();
            var sendMessage = storeService.SendMessage(store_id, 9999, "Hello World!");
            var messages = DBManager.Instance.LoadMessagesPerChat(9999, store_id);
            Assert.IsTrue(messages.Exists(message => message.userId == 9999 && message.storeId == store_id && message.message == "Hello World!"));
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
            var orders = DBManager.Instance.LoadOrders();
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
            var orders = DBManager.Instance.LoadOrders();
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
            var orders = DBManager.Instance.LoadOrders();
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
            var orders = DBManager.Instance.LoadOrders();
            Assert.IsTrue(orders.Exists(order => order.orderID == order_id && !order.active));
            storeService.reOpenOrder(store_id, order_id);
            orders = DBManager.Instance.LoadOrders();
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
            var orders = DBManager.Instance.LoadOrders();
            var store_orders = orders.Where(o => o.storeId == store_id).ToList();
            var order_IDs = store_orders.Select(o => o.orderID).ToList();
            Assert.IsTrue(order_IDs.OrderBy(x => x).SequenceEqual(new List<int> {order1 , order2 }.OrderBy(x => x)));
        }
        
        private static T Cast<T>(T typeHolder, Object x)
        {
            // typeHolder above is just for compiler magic
            // to infer the type to cast x to
            return (T)x;
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
            var orders = DBManager.Instance.LoadOrders();
            // get the orders fron DB
            var store_orders = orders.Where(o => o.storeId == store_id).Select(o => o.orderID).ToList();
            // get the orders from charHistory
            var orderHistory = storeService.SeeOrderHistoryStore(store_id).value;
            // change both store_orders and orderHistory ID's to be of the same types (List<string>)
            var orderHistoryIDs = new List<int>();
            foreach (object o in orderHistory)
            {
                
                //var jsonObject = JObject.Parse(o);
                var jsonObject = new { storeId = 0, storeName = "", orderId = 0, date = DateTime.Now, status = "", memberName = "", memberId = 0, active = true, cost = (Single)0.0, description = "" };
                
                jsonObject = Cast(jsonObject, o);
                orderHistoryIDs.Add(jsonObject.orderId);
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
            var post = storeService.AddPost(store_id, "", "");
            Assert.IsFalse(post.exceptionHasOccured);
        }
        
        [Test]
        public void AddPostFail()
        {
            // store doesn't exist
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var post = storeService.AddPost(8989, "", "");
            Assert.IsTrue(post.exceptionHasOccured);
        }
        
        [Test]
        public void RemovePostSuccess()
        {
            var adminService = AdminS.Instance;
            var storeService = Store.Instance;
            var store_id = adminService.CreateNewStore(0, "my-store", "").value;
            var post_id = storeService.AddPost(store_id, "", "").value.postId;
            var remove = storeService.RemovePost(store_id, post_id);
            Assert.IsFalse(remove.exceptionHasOccured);
        }

    }
}

