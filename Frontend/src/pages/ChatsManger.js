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
import BackButton from '../components/BackButton';
import StoreButton from '../components/StoreButton';
const { startChatUser, startChatStore, sendMessageUser, sendMessageStore, getAllChatsUser, getAllChatsStore }=getChatService()
const {getAllStores} = GetUserService()
export default function ChatManagerPage() {
const [isStoreDataAdded, setIsStoreDataAdded] = useState(false);
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
      await setStores(transformedData);
      console.log(stores)
      console.log('Transformed data:', transformedData);
      // Set the state variable to true after store data is added
      setIsStoreDataAdded(true);
    } catch (error) {
      console.error('Error in addStoreData:', error);
    }
  }
  // Fetch all stores when the component mounts
    const fetchChatsAndStores = async () => {
    const storedUserId = localStorage.getItem('userId');
    const storedUserType = localStorage.getItem('userType');
    console.log(storedUserId,storedUserType)
    if (storedUserId && storedUserType) {
      setUserId(parseInt(storedUserId, 10));
      setUserType(storedUserType);
    }
    
    // If user type is 'store', fetch all chats for the store
    // Otherwise, fetch all chats for the user
    if (storedUserType === 'store') {
     await getAllChatsStore(userId).then(chatData => {
        setChats(chatData.value);
      });
    } else {
      await getAllChatsUser(userId).then(chatData => {
        // Transform each chat with additional data
        let transformedChats = chatData.value.map((chat) => {
          console.log(chat)
          // Parse the chat string into an object
          let chatItems = JSON.parse(chat);
        
          // Find the corresponding store
          let correspondingStore = stores.find(store => store.storeId === chatItems.storeId);
          return {
            sessionId: chatItems.sessionId,
            name: correspondingStore ? correspondingStore.storeName : 'Unknown Store',
            messages: []
          }
        });
        
        setChats(transformedChats);
      });
    }}

    useEffect(() => {
      addStoreData();
    }, []); 
    
    useEffect(() => {
      if (isStoreDataAdded) {
        fetchChatsAndStores();
      }
    }, [isStoreDataAdded, userId])

  const openChatWithStore = async (storeId) => {
    try {
      const newChat = await startChatUser(localStorage.getItem("userId"), storeId);
      console.log(newChat)
      // Check if newChat is in the correct format
      if (!newChat || !newChat.value || !newChat.value.item1 || !newChat.value.item2) {
        throw new Error('New chat data is not in expected format');
      }
      
      // Transform newChat into desired format
      const transformedChat = {
        sessionId: newChat.value.item1,
        name: newChat.value.item2,
        messages:[]
      };
      setChats(prevChats => [...prevChats, transformedChat]);
      
      setOpenDialog(false); // Close the dialog
  
    } catch (error) {
      console.error('Error in openChatWithStore:', error);
    }
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
        <ChatDisplay userId={userId} userType={userType} chats={chats} sendMessage={userType === 'store' ? sendMessageStore : sendMessageUser} />
      ) : (
        <Typography variant="body1">
          Please login as a user or store to access the chat.
        </Typography>
      )}
      <BackButton back></BackButton>
    </Container>
  );
}
