﻿using Backend.Business.src.Utils;
using Backend.Business.src.Reports;
using Backend.Business.Utils;
namespace Backend.Business.src.Client_Store;

public class ClientStoreService
{
    private ChatManager chatManager;
    private OrderManager orderManager;
    private OutputManager outputManager;
    private WorkerManager workerManager;
    private User employee;
    private NotificationManager notificationManager;
    private PageManager pageManager;

    public ClientStoreService(int storeId, int userId, string email)
    {
        chatManager = new ChatManager();
        orderManager = new OrderManager();
        outputManager = new OutputManager();
        workerManager = new WorkerManager();
        notificationManager = new NotificationManager();
        employee = AuthenticationManager.GetInstance().Login(userId, email);

        String storeName = AuthenticationManager.GetInstance().CheckWorkingPrivilege(storeId, userId);
        if (storeName == null)
        {
            //TODO: Stop and raise an exception
            return;
        }
        
        pageManager = new PageManager(storeName);
    }

    public Response<Product> AddProduct(string name, string description)
    {
        Product prod = new Product(name, description);
        return pageManager.AddProduct(prod);
    }

    public Response<int> OpenChat(int userId)
    {
        return chatManager.StartChat(employee.UserId, userId);
    }

    public Response<string> SendMessage(int sessionId, string msg)
    {
        return chatManager.SendMessage(sessionId, new Message<string>(employee.UserId, msg));
    }

    public Response<List<String>> GetAllchats(int storeId)
    {
        return chatManager.GetAllChats(storeId);
    }

    public Response<int> addOrder(int storeID, int memberID, string memberName, string description, float cost)
    {
        return orderManager.addOrder(storeID, memberID, memberName, description, cost);
    }

    public Response<string> changeOrdersStatus (int storeID, int orderID, string status)
    {
        return orderManager.changeOrdersStatus(storeID, orderID, status);

    }



}