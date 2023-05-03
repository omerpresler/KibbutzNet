﻿using System.Collections;
using Backend.Business.src.Utils;
using Backend.Business.src.Reports;
using Backend.Business.Utils;
using Newtonsoft.Json;

namespace Backend.Business.src.Client_Store;

public class ClientStoreService
{
    private int storeId;
    private ChatManager chatManager;
    private OrderManager orderManager;
    private OutputManager outputManager;
    private WorkerManager workerManager;
    private User employee;
    private NotificationManager notificationManager;
    private PageManager pageManager;

    public ClientStoreService(int storeId, int userId, string email)
    {
        this.storeId = storeId;
        chatManager = new ChatManager();
        orderManager = new OrderManager();
        outputManager = new OutputManager();
        workerManager = new WorkerManager();
        notificationManager = new NotificationManager();
        employee = AuthenticationManager.Instance.Login(userId, email);

        String storeName = AuthenticationManager.Instance.CheckWorkingPrivilege(storeId, userId);
        if (storeName == null)
            throw new Exception($"User {userId} does not work at {storeId}");
        
        pageManager = new PageManager(storeName);
    }

    public Response<Product> AddProduct(string name, string description)
    {
        Product prod = new Product(name, description);
        return pageManager.AddProduct(prod);
    }

    public Response<int> OpenChat(int userId)
    {
        return chatManager.StartChat(storeId, userId);
    }

    public Response<string> SendMessage(int sessionId, string msg)
    {
        return chatManager.SendMessage(sessionId, new Message<string>(storeId, msg));
    }

    public Response<List<String>> GetAllchats()
    {
        return chatManager.GetAllStoreChats(storeId);
    }

    public Response<int> addOrder(int memberID, string memberName, string description, float cost)
    {
        return orderManager.addOrder(storeId, memberID, memberName, description, cost);
    }

    public Response<string> changeOrdersStatus (int orderID, string status)
    {
        return orderManager.changeOrdersStatus(storeId, orderID, status);

    }
    
    
    public ArrayList GetPurchasesByUser(int userId)
    {
        ArrayList jsons = new ArrayList();
        foreach (Order o in orderManager.orders[storeId])
            if (o.memberId == userId)
            {
                jsons.Add(JsonConvert.SerializeObject(o));
            }
        return jsons;
    }



}