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
import Store from './Store';
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
      console.log('Transformed data:', transformedData);
      setIsStoreDataAdded(true);
    } catch (error) {
      console.error('Error in addStoreData:', error);
    }
  }

  const fetchChats = async () => {
    const storedUserId = localStorage.getItem('userId');
    const storedUserType = localStorage.getItem('userType');

    if (storedUserId && storedUserType) {
      setUserId(parseInt(storedUserId, 10));
      setUserType(storedUserType);
    }

    let storeNameToId = {};
    stores.forEach(store => {
        storeNameToId[store.storeName] = store.storeId;
    });

    if (storedUserType === 'store') {
      await getAllChatsStore(userId).then(chatData => {
        console.log(chatData);
        let transformedChats = chatData.value.map((chatString) => {
          let chat = JSON.parse(chatString);
          return {
            userId: chat.userId,
            storeId: localStorage.getItem("storeId"),
            name: chat.User,
            messages: chat.Messages || []
          }
        });
        setChats(transformedChats);
      });
    } else {
      await getAllChatsUser(userId).then(chatData => {
        console.log(chatData);
        let transformedChats = chatData.value.map((chatString) => {
          let chat = JSON.parse(chatString);
          let storeId = storeNameToId[chat.Store];
          return {
            storeId: storeId,
            userId: localStorage.getItem("userId"),
            name: chat.Store,
            messages: chat.Messages || []
          }
        });

        stores.forEach(store => {
          if (!transformedChats.some(chat => chat.storeId === store.storeId)) {
            transformedChats.push({
              userId: localStorage.getItem("userId"),
              storeId: store.storeId,
              name: store.storeName,
              messages: []
            });
          }
        });

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
      <Box sx={{ mt: 2 }}></Box>
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
