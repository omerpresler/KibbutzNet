// src/components/ChatDisplay.js
import Message from '../services/dataObjects/Message';
import React, { useState, useEffect } from 'react';
import {
  Box,
  List,
  ListItem,
  ListItemText,
  TextField,
  Button,
} from '@mui/material';
import ChatService from '../services/ChatService';

export default function ChatDisplay({ userId, userType }) {
  const [chats, setChats] = useState([]);
  const [selectedChat, setSelectedChat] = useState(null);
  const [messages, setMessages] = useState([]);
  const [newMessage, setNewMessage] = useState('');
  const chatService = ChatService();

  useEffect(() => {
    const fetchChats = async () => {
      const response =
        userType === 'user'
          ? await chatService.getAllChatsUser(userId)
          : await chatService.getAllChatsStore(userId);

      if (!response.exception) {
        setChats(response.value);
      }
    };

    fetchChats();
  }, [userId, userType, chatService]);

  const handleChatSelect = (chat) => {
    setSelectedChat(chat);
    setMessages(chat.messages);
  };

  const handleSendMessage = async () => {
    if (newMessage.trim() === '') return;

    const message = new Message(userId, newMessage);
    const response = await chatService.sendMessage(selectedChat.sessionId, message);

    if (!response.exception) {
      setMessages([...messages, message]);
      setNewMessage('');
    }
  };

  return (
    <Box>
      {chats.map((chat) => (
        <Box key={chat.sessionId} onClick={() => handleChatSelect(chat)}>
          {chat.sessionId}
        </Box>
      ))}
      {selectedChat && (
        <Box>
          <List>
            {messages.map((message, index) => (
              <ListItem key={index}>
                <ListItemText
                  primary={message.sender === userId ? 'You' : 'Them'}
                  secondary={message.message}
                />
              </ListItem>
            ))}
          </List>
          <TextField
            value={newMessage}
            onChange={(e) => setNewMessage(e.target.value)}
            fullWidth
          />
          <Button onClick={handleSendMessage}>Send</Button>
        </Box>
      )}
    </Box>
  );
}
