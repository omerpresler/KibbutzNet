// AdminPage.js
import Center from "../components/Center";
import { Button, Card, CardContent, TextField, Typography } from '@mui/material';
import axios from 'axios';
import {useNavigate} from 'react-router-dom';
import { Box } from '@mui/system';
import { useState } from 'react';
import useForm from '../hooks/useFrom';
import getAdminService from '../services/AdminService';
import BackButton from "../components/BackButton";

export default function AdminPage() {
  const navigate = useNavigate();
  const{addNewUser,addNewStore,connectStoreUser} = getAdminService();

  const [showAddUserForm, setShowAddUserForm] = useState(false);
  const [showAddStoreForm, setShowAddStoreForm] = useState(false);
  const [showConnectStoreUserForm, setShowConnectStoreUserForm] = useState(false);

  const userFormModel = () => ({
    userId: '',
    name: '',
    phoneNumber: '',
    email: ''
  });

  const storeFormModel = () => ({
    storeName: '',
    photoLink: ''
  });

  const connectStoreUserFormModel = () => ({
    userId: '',
    storeId: ''
  });

  const {
    values: userFormValues,
    setValues: setUserFormValues,
    errors: userFormErrors,
    setErrors: setUserFormErrors,
    handleInputChange: handleUserFormInputChange
  } = useForm(userFormModel);

  const {
    values: storeFormValues,
    setValues: setStoreFormValues,
    errors: storeFormErrors,
    setErrors: setStoreFormErrors,
    handleInputChange: handleStoreFormInputChange
  } = useForm(storeFormModel);

  const {
    values: connectFormValues,
    setValues: setConnectFormValues,
    errors: connectFormErrors,
    setErrors: setConnectFormErrors,
    handleInputChange: handleConnectFormInputChange
  } = useForm(connectStoreUserFormModel);

  const handleSubmitUserForm = async (event) => {
    event.preventDefault();
    const ServerAns=await addNewUser(userFormValues.userId,userFormValues.name,userFormValues.phoneNumber,userFormValues.email)
  };

   const handleSubmitStoreForm = async (event) => {
    event.preventDefault();
    const ServerAns=await addNewStore(storeFormValues.userId,storeFormValues.storeName,storeFormValues.photoLink)
  };

   const handleSubmitConnectForm = async (event) => {
    event.preventDefault();
    const ServerAns=await connectStoreUser(connectFormValues.userId,connectFormValues.storeId)
  };

  return (
    <Center>
      <Card sx={{ width: 1000 }}>
        <CardContent sx={{ textAlign: 'center' }}>
          <Typography variant="h2" sx={{ my: 3 }}>
            עמוד מנהל
          </Typography>

          <Button onClick={() => {
            setShowAddUserForm(true);
            setShowAddStoreForm(false);
            setShowConnectStoreUserForm(false);
          }}>הוסף חבר קיבוץ</Button>

          <Button onClick={() => {
            setShowAddStoreForm(true);
            setShowAddUserForm(false);
            setShowConnectStoreUserForm(false);
          }}>הוסף חנות</Button>

          <Button onClick={() => {
            setShowConnectStoreUserForm(true);
            setShowAddUserForm(false);
            setShowAddStoreForm(false);
          }}>קישור בין חנות לעובד חנות</Button>


          {showAddUserForm && (
  <form onSubmit={handleSubmitUserForm}>
   
    <TextField
      label="מספר יוזר"
      name="userId"
      value={userFormValues.userId}
      onChange={handleUserFormInputChange}
      variant="outlined"
      {...(userFormErrors.userId && { error: true, helperText: userFormErrors.userId })}
    />
    <TextField
      label="שם"
      name="name"
      value={userFormValues.name}
      onChange={handleUserFormInputChange}
      variant="outlined"
      {...(userFormErrors.name && { error: true, helperText: userFormErrors.name })}
    />
    <TextField
      label="מספר טלפון"
      name="phoneNumber"
      value={userFormValues.phoneNumber}
      onChange={handleUserFormInputChange}
      variant="outlined"
      {...(userFormErrors.phoneNumber && { error: true, helperText: userFormErrors.phoneNumber })}
    />
    <TextField
      label="איימל"
      name="email"
      value={userFormValues.email}
      onChange={handleUserFormInputChange}
      variant="outlined"
      {...(userFormErrors.email && { error: true, helperText: userFormErrors.email })}
    />
    <Button type="submit">Submit</Button>
  </form>
)}


{showAddStoreForm && (
  <form onSubmit={handleSubmitStoreForm}>
  
    <TextField
      label="שם החנות"
      name="storeName"
      value={storeFormValues.storeName}
      onChange={handleStoreFormInputChange}
      variant="outlined"
      {...(storeFormErrors.storeName && { error: true, helperText: storeFormErrors.storeName })}
    />
     <TextField
      label="קישור לתמונה"
      name="photoLink"
      value={storeFormValues.photoLink}
      onChange={handleStoreFormInputChange}
      variant="outlined"
      {...(storeFormErrors.storeName && { error: true, helperText: storeFormErrors.storeName })}
    />
    <Button type="submit">Submit</Button>
  </form>
)}


{showConnectStoreUserForm && (
  <form onSubmit={handleSubmitConnectForm}>
    <TextField
      label="מספר יוזר"
      name="userId"
      value={connectFormValues.userId}
      onChange={handleConnectFormInputChange}
      variant="outlined"
      {...(connectFormErrors.userId && { error: true, helperText: connectFormErrors.userId })}
    />
    <TextField
      label="מספר חנות"
      name="storeId"
      value={connectFormValues.storeId}
      onChange={handleConnectFormInputChange}
      variant="outlined"
      {...(connectFormErrors.storeId && { error: true, helperText: connectFormErrors.storeId })}
    />
    <Button type="submit">Submit</Button>
  </form>
)}
  <BackButton back></BackButton>
        </CardContent>
      </Card>
    </Center>
  );
}

