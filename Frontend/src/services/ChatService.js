// src/services/ChatService.js
import axios from 'axios';
import { Response } from './Response';
import * as paths from '../services/pathes';



export default function ChatService() {
  function startChat(sender, target) {
    return axios
      .post(paths.startChatPath, { sender, target })
      .then((response) => {
        return Response.create(response.data.value, response.data.wasExecption);
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
        return Response.create(response.data.value, response.data.wasExecption);
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
        return Response.create(response.data.value, response.data.wasExecption);
      })
      .catch((error) => {
        console.log('Error sending message:', error);
        return Response.create(null, true, error.message);
      });
  }

  function getAllChats(userId) {
    return axios
      .post(paths.getAllChatsPath, { userId })
      .then((response) => {
        return Response.create(response.data.value, response.data.wasExecption);
      })
      .catch((error) => {
        console.log('Error getting all chats:', error);
        return Response.create(null, true, error.message);
      });
  }

  return { startChat, endChat, sendMessage, getAllChats };
}