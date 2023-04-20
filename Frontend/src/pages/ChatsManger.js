// src/pages/ChatPage.js

import React, { useState, useEffect } from 'react';
import { Box, Container, Typography } from '@mui/material';
import ChatDisplay from '../components/ChatDisplyer';
import ChatService from '../services/ChatService';
import BackButton from '../components/BackButton';

export default function ChatManagerPage() {
  const [userId, setUserId] = useState(null);
  const [userType, setUserType] = useState(null);
  const chatService = ChatService();

  useEffect(() => {
    // Fetch user ID and user type from local storage or other sources
    const storedUserId = localStorage.getItem('userId');
    const storedUserType = localStorage.getItem('userType');
    if (storedUserId && storedUserType) {
      setUserId(parseInt(storedUserId, 10));
      setUserType(storedUserType);
    }
  }, []);

  return (
    <Container>
      <Box sx={{ mt: 4 }}>
        <Typography variant="h4">Chat</Typography>
      </Box>
      {userId && userType ? (
        <ChatDisplay userId={userId} userType={userType} />
      ) : (
        <Typography variant="body1">
          Please login as a user or store to access the chat.
        </Typography>
      )}
    </Container>
  );
}