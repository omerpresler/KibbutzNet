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
}
from '@mui/material';
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
   // Fetch all stores when the component mounts
const fetchChats = async () => {
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
      console.log(chatData)
      // Transform each chat with additional data
      let transformedChats = chatData.value.map((chatString) => {
        // Parse the chat JSON string into an object
        let chat = JSON.parse(chatString);
  
        // Find the corresponding user
        
  
        return {
          userId: chat.userId,
          storeId: localStorage.getItem("storeId"),  // Assuming the storeId is stored in localStorage
          name: chat.User,
          messages: chat.Messages || []  // Assuming chat.Messages contains the list of messages
        }
      });
      setChats(transformedChats);
    });
  } else {
    await getAllChatsUser(userId).then(chatData => {
      // Transform each chat with additional data
      let transformedChats = chatData.value.map((chatString) => {
        // Parse the chat JSON string into an object
        let chat = JSON.parse(chatString);
        // Find the corresponding store
        console.log(stores)
        let correspondingStore = stores.find(store => store.storeId === chat.Store);
        
        return {
          storeId: chat.Store,
          userId: localStorage.getItem("userId"),
          name: correspondingStore ? correspondingStore.storeName : 'Unknown Store',
          messages: chat.Messages || []  // Assuming chat.Messages contains the list of messages
        }
      });
      console.log(chatData)
    
      // For each store that doesn't have a chat yet, create an empty chat
      stores.forEach(store => {
        console.log(store)
        if (!transformedChats.some(chat => chat.storeId === store.storeId)) {
          transformedChats.push({
            userId: localStorage.getItem("userId"),
            storeId: store.storeId,
            name: store.storeName,
            messages: []
          });
        }
      });
      console.log(transformedChats)
      setChats(transformedChats);
    });
    
    
  }
}


    useEffect(() => {
      addStoreData();
    }, []); 
    
    useEffect(() => {
      if (isStoreDataAdded) {
        fetchChats();
      }
    }, [isStoreDataAdded, userId])


  const handleOpenNewChat = () => {
    setOpenDialog(true);
  };

  const handleCloseDialog = () => {
    setOpenDialog(false);
  };

  return (
    <Container>
      <Box sx={{ mt: 4 }}>
        <Typography variant="h4">מנהל שיחות</Typography>
      </Box>
      <Box sx={{ mt: 2 }}>
      </Box>
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
