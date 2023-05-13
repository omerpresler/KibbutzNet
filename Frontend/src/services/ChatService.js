// src/services/ChatService.js
import axios from 'axios';
import { Response } from './Response';
import * as paths from '../services/pathes';
import Message from './data objects/Message';


export default function getChatService() {
  function startChat(sender, target) {
    return axios
      .post(paths.startChatPath, { sender, target })
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage)
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured,response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error starting chat:', error);
        return Response.create(null, true, error.message);
      });
  }

  function endChat(sessionId) {
    return axios
      .post(paths.endChatPath, { sessionId })
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage)
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured,response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error ending chat:', error);
        return Response.create(null, true, error.message);
      });
  }

  function sendMessage(sessionId, message) {
    return axios
      .post(paths.sendMessagePath, { sessionId, message })
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage)
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured,response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error sending message:', error);
        return Response.create(null, true, error.message);
      });
  }

    function getAllChatsUser(userId) {
      const fakeData = [
        {
          sessionId: 1,
          source: userId,
          target: 2,
          active: true,
          messages: [
            new Message(userId, 'Hello!'),
            new Message(2, 'Hi, how can I help you?'),
            new Message(userId, 'I have a question about my order.'),
          ],
        },
        {
          sessionId: 2,
          source: userId,
          target: 3,
          active: true,
          messages: [
            new Message(userId, 'Hi, I need assistance.'),
            new Message(3, 'Sure, what do you need help with?'),
          ],
        },
      ];
    
      const response = Response.create(fakeData, false);
      return Promise.resolve(response);
    return axios
      .post(paths.getAllChatsPath, { userId })
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage)
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured,response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error getting all chats:', error);
        return Response.create(null, true, error.message);
      });
  }


      function getAllChatsStore(storeId) {
      const fakeData = [
        {
          sessionId: 1,
          source: storeId,
          target: 2,
          active: true,
          messages: [
            new Message(storeId, 'Hello!'),
            new Message(2, 'Hi, how can I help you?'),
            new Message(storeId, 'I have a question about my order.'),
          ],
        },
        {
          sessionId: 2,
          source: storeId,
          target: 3,
          active: true,
          messages: [
            new Message(storeId, 'Hi, I need assistance.'),
            new Message(3, 'Sure, what do you need help with?'),
          ],
        },
      ];
      const response = Response.create(fakeData, false);
      return Promise.resolve(response);
    return axios
      .post(paths.getAllChatsPath, { storeId })
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage)
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured,response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error getting all chats:', error);
        return Response.create(null, true, error.message);
      });
  }

  return { startChat, endChat, sendMessage, getAllChatsUser,getAllChatsStore };
}