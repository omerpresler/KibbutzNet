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
  unstable_ClassNameGenerator,
}  from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import getChatService from '../services/ChatService';
import Center from './Center';

export default function ChatDisplyer({ userId, userType,chats,sendMessage }) {
  const [selectedChat, setSelectedChat] = useState([]);
  const [messages, setMessages] = useState([]);
  const [newMessage, setNewMessage] = useState('');
 

  const handleChatSelect = (chat) => {
    console.log(chat)
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
    console.log(selectedChat)
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
            <Paper elevation={3} sx={{ padding: '1rem', height: 'calc(100vh - 64px)', overflowY: 'auto' }}>
              <Typography variant="h5" gutterBottom sx={{ marginBottom: 2, color: '#3f51b5' }}>
                Chats
              </Typography>
              <List>
                {chats.map((chat) => (
                  <ListItem
                    key={chat.sessionId}
                    button
                    onClick={() => handleChatSelect(chat)}
                    sx={{
                      backgroundColor: selectedChat?.sessionId === chat.sessionId ? '#f0f0f0' : '',
                      '&:hover': {
                        backgroundColor: '#f1f1f1',
                      },
                    }}
                  >
                    <ListItemAvatar>
                      <Avatar>{chat.name}</Avatar>
                    </ListItemAvatar>
                    <ListItemText primary={chat.name} />
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
              <Paper elevation={3} sx={{ padding: '1rem', height: 'calc(100vh - 64px)', overflowY: 'auto' }}>
                <Typography variant="h5" gutterBottom sx={{ marginBottom: 2, color: '#3f51b5' }}>
                  Chat name: {selectedChat.name}
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
                <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-end', mt: 2 }}>
                  <TextField
                    value={newMessage}
                    onChange={(e) => setNewMessage(e.target.value)}
                    fullWidth
                    variant="outlined"
                    margin="normal"
                  />
                  <Button
                    onClick={handleSendMessage}
                    variant="contained"
                    sx={{ mt: 1, mb: 1, backgroundColor: '#3f51b5', '&:hover': { backgroundColor: '#303f9f' } }}
                  >
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