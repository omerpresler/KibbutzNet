import React, { useState } from 'react';
import { Button, TextField, Typography } from '@mui/material';
import { Box } from '@mui/system';
import ChatService from '../services/ChatService';

export default function Chat({ userId, userType }) {
  const [message, setMessage] = useState('');
  const [chatLog, setChatLog] = useState([]);
  const chatService = ChatService();

  const handleChange = (event) => {
    setMessage(event.target.value);
  };

  const handleSend = async () => {
    const response = await chatService.sendMessage(/* sessionId, message */);
    // Update chatLog with the new message
  };

  const handleGetChats = async () => {
    const response = await chatService.getAllChats(userId, userType);
    // Update chatLog with the fetched chats
  };

  return (
    <Box>
      <Typography variant="h4">Chat</Typography>
      {/* Display chatLog here */}
      <TextField
        fullWidth
        variant="outlined"
        label="Message"
        value={message}
        onChange={handleChange}
      />
      <Button variant="contained" onClick={handleSend}>
        Send
      </Button>
      <Button variant="contained" onClick={handleGetChats}>
        Get Chats
      </Button>
    </Box>
  );
}