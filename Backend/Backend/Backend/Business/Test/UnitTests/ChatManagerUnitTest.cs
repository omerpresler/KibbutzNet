using System.Drawing;
using Backend.Business.src.Utils;
using Backend.Business.Utils;

namespace Backend.Business.Test.UnitTests;
using NUnit.Framework;
using Sandbox;

public class ChatManagerUnitTest
{
    [Test]
    public void StartChat()
    {
        ChatManager cm = new ChatManager();
        Member sender = new Member(1,"Sender1", "051-111-1111", "email");
        Member recipient = new Member(2,"recipient1", "051-222-2222", "email");
        cm.StartChat(1, 1);

        Console.WriteLine(cm.GetChats().Count);

        Assert.AreEqual(1, cm.GetChats().Count);
        
        cm.StartChat(1, 1);
        
        Assert.AreEqual(cm.GetChats().Count, 2);
        
        Assert.AreNotEqual(cm.GetChats().ToArray()[0].sessionId, cm.GetChats().ToArray()[1].sessionId);
    }
    
    [Test]
    public void EndChat()
    {
        ChatManager cm = new ChatManager();
        Member sender = new Member(1, "Sender1", "051-111-1111", "email");
        Member recipient = new Member(2, "recipient1", "051-222-2222", "email");
        
        Response<int> firstSession = cm.StartChat(1, 1);
        Response<int> secondSession = cm.StartChat(1, 1);
        
        Assert.AreNotEqual(firstSession.value,secondSession.value);
        
        Assert.AreEqual(cm.GetChats().Count, 2);

        cm.EndChat(firstSession.value);
        Assert.AreEqual(cm.GetChats().Count, 1);
        cm.EndChat(firstSession.value);
        Assert.AreEqual(cm.GetChats().Count, 1);
        cm.EndChat(-1);
        Assert.AreEqual(cm.GetChats().Count, 1);
        cm.EndChat(secondSession.value);
        Assert.AreEqual(cm.GetChats().Count, 0);
    }

    [Test]
    public void SendTextMessage()
    {
        
    }
}