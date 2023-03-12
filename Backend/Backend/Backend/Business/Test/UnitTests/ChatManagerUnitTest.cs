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
        Member sender = new Member(1, 1, "Sender1", "051-111-1111");
        Member recipient = new Member(2, 2, "recipient1", "051-222-2222");
        cm.StartChat(sender, recipient);

        Console.WriteLine(cm.getChats().Count);

        Assert.AreEqual(1, cm.getChats().Count);
        
        cm.StartChat(recipient, sender);
        
        Assert.AreEqual(cm.getChats().Count, 2);
        
        Assert.AreNotEqual(cm.getChats().ToArray()[0].sessionId, cm.getChats().ToArray()[1].sessionId);
    }
    
    [Test]
    public void EndChat()
    {
        ChatManager cm = new ChatManager();
        Member sender = new Member(1, 1, "Sender1", "051-111-1111");
        Member recipient = new Member(2, 2, "recipient1", "051-222-2222");
        
        Response<int> firstSession = cm.StartChat(sender, recipient);
        Response<int> secondSession = cm.StartChat(recipient, sender);
        
        Assert.AreNotEqual(firstSession.value,secondSession.value);
        
        Assert.AreEqual(cm.getChats().Count, 2);

        cm.EndChat(firstSession.value);
        Assert.AreEqual(cm.getChats().Count, 1);
        cm.EndChat(firstSession.value);
        Assert.AreEqual(cm.getChats().Count, 1);
        cm.EndChat(-1);
        Assert.AreEqual(cm.getChats().Count, 1);
        cm.EndChat(secondSession.value);
        Assert.AreEqual(cm.getChats().Count, 0);
    }

    [Test]
    public void SendTextMessage()
    {
        ChatManager cm = new ChatManager();
        Member member1 = new Member(1, 1, "Sender1", "051-111-1111");
        Member member2 = new Member(2, 2, "recipient1", "051-222-2222");
        

        int firstSession = cm.StartChat(member1, member2).value;
        Message<String> msg1 = new Message<String>(member1, "First message", "random Addon");
        Message<House> msg2 = new Message<House>(member1, "Second message", new House(1, "BlaBla1"));
        Message<String> msg3 = new Message<String>(member2, "Third message", "random Addon 2");
        Message<House> msg4 = new Message<House>(member2, "fourth message", new House(2, "BlaBla2"));

        cm.SendMessage(firstSession, msg1);
        cm.SendMessage(firstSession, msg2);
        cm.SendMessage(firstSession, msg3);
        cm.SendMessage(firstSession, msg4);

        Assert.AreEqual(cm.chats.Count, 1);
        Assert.AreEqual(cm.chats.ToArray()[0].messages.Count, 4);
        Assert.AreEqual(cm.chats.ToArray()[0].messages.ToArray()[0], msg1);
        Assert.AreEqual(cm.chats.ToArray()[0].messages.ToArray()[1], msg2);
        Assert.AreEqual(cm.chats.ToArray()[0].messages.ToArray()[2], msg3);
        Assert.AreEqual(cm.chats.ToArray()[0].messages.ToArray()[3], msg4);

        Assert.AreEqual(msg1.sender, member1);
        Assert.AreEqual(msg2.sender, member1);
        Assert.AreEqual(msg3.sender, member2);
        Assert.AreEqual(msg4.sender, member2);

        Assert.AreEqual(msg1.message, "First message");
        Assert.AreEqual(msg2.message, "Second message");
        Assert.AreEqual(msg3.message, "Third message");
        Assert.AreEqual(msg4.message, "fourth message");
    }
}