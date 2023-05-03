// src/components/ChatDisplay.js
import Message from '../services/data objects/Message';
import React, { useState, useEffect } from 'react';
import {
  Box,
  List,
  ListItem,
  ListItemText,
  TextField,
  Button,
  Typography,
  Paper,
  ListItemAvatar,
  Avatar,
  IconButton,
  Grid,
  Container,
}  from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import getChatService from '../services/ChatService';
import Center from './Center';

export default function ChatDisplyer({ userId, userType }) {
  const [chats, setChats] = useState([]);
  const [selectedChat, setSelectedChat] = useState([]);
  const [messages, setMessages] = useState([]);
  const [newMessage, setNewMessage] = useState('');
  const { startChat, endChat, sendMessage, getAllChatsUser, getAllChatsStore } = getChatService();

  useEffect(() => {
    const fetchChats = async () => {
      const response =
        userType === 'user'
          ? await getAllChatsUser(userId)
          : await getAllChatsStore(userId);

      if (!response.exception) {
        setChats(response.value);
      }
    };

    fetchChats();
  }, [userId, userType, messages]);

  const handleChatSelect = (chat) => {
    setSelectedChat(chat);
    setMessages(chat.messages);
  };

  const handleCloseChat = () => {
    setSelectedChat(null);
    setMessages([]);
  };

  const handleSendMessage = async () => {
    if (newMessage.trim() === '') return;

    const message = new Message(userId, newMessage);
    const response = await sendMessage(selectedChat.sessionId, message);

    if (!response.exception) {
      setMessages([...messages, message]);
      setNewMessage('');
    }
  };

  return (
    <Center>
      <Container maxWidth="md">
        <Grid container spacing={3}>
          <Grid item xs={12} md={4}>
            <Paper elevation={3} style={{ padding: '1rem', height: 'calc(100vh - 64px)', overflowY: 'auto' }}>
              <Typography variant="h5" gutterBottom>
                Chats
              </Typography>
              <List>
                {chats.map((chat) => (
                  <ListItem
                    key={chat.sessionId}
                    button
                    onClick={() => handleChatSelect(chat)}
                    style={{ backgroundColor: selectedChat?.sessionId === chat.sessionId ? 'black' : '' }}
                  >
                    <ListItemAvatar>
                      <Avatar>{chat.sessionId}</Avatar>
                    </ListItemAvatar>
                    <ListItemText primary={`Chat ID: ${chat.sessionId}`} />
                    <IconButton onClick={handleCloseChat} edge="end" aria-label="close">
                      <CloseIcon />
                    </IconButton>
                  </ListItem>
                ))}
              </List>
            </Paper>
          </Grid>
          {selectedChat && (
            <Grid item xs={12} md={8}>
              <Paper elevation={3} style={{ padding: '1rem', height: 'calc(100vh - 64px)', overflowY: 'auto' }}>
                <Typography variant="h5" gutterBottom>
                  Chat ID: {selectedChat.sessionId}
                </Typography>
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
                <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-end' }}>
                  <TextField
                    value={newMessage}
                    onChange={(e) => setNewMessage(e.target.value)}
                    fullWidth
                    variant="outlined"
                    margin="normal"
                  />
                  <Button onClick={handleSendMessage} variant="contained" sx={{ mt: 1, mb: 1 }}>
                    Send
                  </Button>
                </Box>
              </Paper>
            </Grid>
          )}
        </Grid>
      </Container>
    </Center>
  );
}