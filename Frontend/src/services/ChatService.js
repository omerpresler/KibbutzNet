// src/services/ChatService.js
import axios from 'axios';
import { Response } from './Response';
import { startChatUserPath, startChatStorePath, sendMessageUserPath, sendMessageStorePath, getAllUserChats, getAllStoreChats } from '../services/pathes';
import Message from './data objects/Message';


export default function getChatService() {
  function startChatUser(sender, target) {
    return axios
      .post(startChatUserPath, { sender, target })
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage)
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured,response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error starting user chat:', error);
        return Response.create(null, true, error.message);
      });
  }

  function startChatStore(sender, target) {
    return axios
      .post(startChatStorePath, { sender, target })
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage)
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured,response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error starting store chat:', error);
        return Response.create(null, true, error.message);
      });
  }

  function sendMessageUser(userId,storeId, message) {
    console.log(storeId,userId)
    console.log(message)
    return axios
      .post(sendMessageUserPath, { userId,storeId,text:message })
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage)
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured,response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error sending user message:', error);
        return Response.create(null, true, error.message);
      });
  }

  function sendMessageStore(userId,storeId, message) {
    console.log(userId,storeId,message)
    return axios
      .post(sendMessageStorePath, { userId,storeId,text:message})
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage)
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured,response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error sending store message:', error);
        return Response.create(null, true, error.message);
      });
  }

  function getAllChatsUser(userId) {
    return axios
      .post(getAllUserChats, { userId })
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage)
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured,response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error getting all user chats:', error);
        return Response.create(null, true, error.message);
      });
  }

  function getAllChatsStore(storeId) {
    return axios
      .post(getAllStoreChats, { storeId })
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage)
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured,response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error getting all store chats:', error);
        return Response.create(null, true, error.message);
      });
  }

  return { startChatUser, startChatStore, sendMessageUser, sendMessageStore, getAllChatsUser, getAllChatsStore };
}
