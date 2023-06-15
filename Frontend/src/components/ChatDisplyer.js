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
 
} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import getChatService from '../services/ChatService';
import Center from './Center';

export default function ChatDisplay({ userId, userType, chats, sendMessage }) {
  const [selectedChat, setSelectedChat] = useState(null);
  const [newMessage, setNewMessage] = useState('');
  const handleChatSelect = async (chat) => {
    await setSelectedChat(chat);
  };


  const handleSendMessage = async () => {
    if (newMessage.trim() === '') return;
    console.log(selectedChat)
    const message = {
      message: newMessage,
      FromMe: true,
    };
  
    const response = await sendMessage(selectedChat.userId, selectedChat.storeId, newMessage);
  
    if (!response.exception) {
      const updatedChat = {
        ...selectedChat,
        messages: [...selectedChat.messages, message],
      };
      setSelectedChat(updatedChat);
      setNewMessage('');
    }
  };

  return (
    <Center>
      <Container maxWidth="md">
        <Grid container spacing={3}>
          <Grid item xs={12} md={selectedChat ? 4 : 12}>
            <Paper elevation={3} sx={{ padding: '1rem', height: 'calc(100vh - 64px)' }}>
             <Typography variant="h5" gutterBottom sx={{ marginBottom: 2, color: '#3f51b5' }}>
                שיחות
              </Typography>
              <List>
                {chats.map((chat) => (
                  <ListItem
                    key={chat.storeId}
                    onClick={() => handleChatSelect(chat)}
                    sx={{
                      backgroundColor: selectedChat?.storeId === chat.storeId ? '#000EE' : '',
                      '&:hover': {
                        backgroundColor: '#f1f1f1',
                      },
                    }}
                  >
                    <ListItemAvatar>
                      <Avatar>{chat.name}</Avatar>
                    </ListItemAvatar>
                    <ListItemText primary={chat.name} />
                  </ListItem>
                ))}
              </List>
            </Paper>
          </Grid>
          {selectedChat && (
            <Grid item xs={12} md={8}>
              <Paper elevation={3} sx={{ padding: '1rem', height: 'calc(100vh - 64px)', overflowY: 'auto' }}>
                <Typography variant="h5" gutterBottom sx={{ marginBottom: 2, color: '#3f51b5' }}>
                  {selectedChat.name}
                </Typography>
          <List>
      {selectedChat.messages.map((message, index) => (
        <ListItem key={index}>
          <ListItemText
            sx={{
              '& .MuiListItemText-primary': {
                textAlign: message.FromMe ? 'left' : 'right',
              },
              '& .MuiListItemText-secondary': {
                textAlign: message.FromMe ? 'left' : 'right',
              },
            }}
            primary={message.FromMe ? 'אני' : 'הם'}
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
                    שלח
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