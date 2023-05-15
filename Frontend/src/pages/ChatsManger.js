// src/pages/ChatPage.js
import React, { useState, useEffect } from 'react';
import GetUserService from '../services/UserService';
import getChatService from '../services/ChatService';
import {
  Box,
  Container,
  Typography,
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  TextField,
  DialogActions,
} from '@mui/material';
import ChatDisplay from '../components/ChatDisplyer';
import ChatService from '../services/ChatService';
import BackButton from '../components/BackButton';
import StoreButton from '../components/StoreButton';
const { startChatUser, startChatStore, sendMessageUser, sendMessageStore, getAllChatsUser, getAllChatsStore }=getChatService()

const {getAllStores} = GetUserService()
export default function ChatManagerPage() {
  const [userId, setUserId] = useState(null);
  const [userType, setUserType] = useState(null);
  const [openDialog, setOpenDialog] = useState(false);
  const [stores, setStores] = useState([]);
  const [chats, setChats] = useState([]);
  async function addStoreData() {
    try {
      let storeData = await getAllStores();
      console.log('Store data:', storeData);
      
      if (!storeData || !Array.isArray(storeData.value)) {
        throw new Error('Store data is not in expected format');
      }
      
      // Transform each store with additional data
      let transformedData = storeData.value.map((store) => {
        return {
          storeId: store.item1,
          storeName: store.item2,
          photoLink: store.item3
        };
      });
      setStores(transformedData);
      console.log('Transformed data:', transformedData);
    } catch (error) {
      console.error('Error in addStoreData:', error);
    }
  }
  
  // Fetch all stores when the component mounts

  useEffect(() => {
    const fetchChatsAndStores = async () => {
    const storedUserId = localStorage.getItem('userId');
    const storedUserType = localStorage.getItem('userType');
    console.log(storedUserId,storedUserType)
    if (storedUserId && storedUserType) {
      setUserId(parseInt(storedUserId, 10));
      setUserType(storedUserType);
    }
  
    addStoreData()
    // If user type is 'store', fetch all chats for the store
    // Otherwise, fetch all chats for the user
    if (storedUserType === 'store') {
     await getAllChatsStore(userId).then(chatData => {
        setChats(chatData.value);
      });
    } else {
      await getAllChatsUser(userId).then(chatData => {
        setChats(chatData.value);
        console.log(chats)
      });
    }}
    fetchChatsAndStores();
  }, [userId]);

   const openChatWithStore = async (storeId) => {
    // Open a chat with the store
    // e.g. ChatService.openNewChat(storeId);
    // Then add this chat to the list of chats
    const newChat = await startChatUser(localStorage.getItem("userId"),storeId) // Replace this with the actual chat object
    setChats([...chats, newChat]);
    console.log(chats)
    setOpenDialog(false); // Close the dialog
  };

  const handleOpenNewChat = () => {
    setOpenDialog(true);
  };

  const handleCloseDialog = () => {
    setOpenDialog(false);
  };

  return (
    <Container>
      <Box sx={{ mt: 4 }}>
        <Typography variant="h4">Chat</Typography>
      </Box>
      <Box sx={{ mt: 2 }}>
        <Button variant="contained" onClick={handleOpenNewChat}>
          Open New Chat
        </Button>
      </Box>
      <Dialog open={openDialog} onClose={handleCloseDialog}>
        <DialogTitle>Open New Chat</DialogTitle>
        <DialogContent>
          <DialogContentText>Please select a store:</DialogContentText>
          {stores
            .filter((store) => !chats.some((chat) => chat.storeId === store.id))
            .map((store) => (
              <StoreButton key={store.id} store={store} onClick={openChatWithStore} />
            ))}
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseDialog}>Cancel</Button>
        </DialogActions>
      </Dialog>
      {userType ? (
        <ChatDisplay userId={userId} userType={userType} chats={chats} />
      ) : (
        <Typography variant="body1">
          Please login as a user or store to access the chat.
        </Typography>
      )}
      <BackButton back></BackButton>
    </Container>
  );
}
