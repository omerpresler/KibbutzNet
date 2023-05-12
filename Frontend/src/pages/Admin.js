// AdminPage.js
import Center from "../components/Center";
import { Button, Card, CardContent, TextField, Typography } from '@mui/material';
import axios from 'axios';
import {useNavigate} from 'react-router-dom';
import { Box } from '@mui/system';
import { useState } from 'react';
import useForm from '../hooks/useForm';
import getAdminService from '../services/AdminService';

export default function AdminPage() {
  const navigate = useNavigate();
  const{addNewUser,addNewStore,connectStoreUser} = getAdminService();

  const [showAddUserForm, setShowAddUserForm] = useState(false);
  const [showAddStoreForm, setShowAddStoreForm] = useState(false);
  const [showConnectStoreUserForm, setShowConnectStoreUserForm] = useState(false);

  const userFormModel = () => ({
    adminId: '',
    userId: '',
    name: '',
    phoneNumber: '',
    email: ''
  });

  const storeFormModel = () => ({
    adminId: '',
    storeId: '',
    storeName: ''
  });

  const connectStoreUserFormModel = () => ({
    adminId: '',
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
    const ServerAns=addNewUser(userFormValues.adminId,userFormValues.userId,userFormValues.name,userFormValues.phoneNumber,userFormValues.email)
  };



  const handleSubmitStoreForm = async (event) => {
    event.preventDefault();
    const ServerAns=addNewStore(storeFormValues.adminId,storeFormValues.storeName)

  };

  const handleSubmitConnectForm = async (event) => {
    event.preventDefault();
    const ServerAns=connectStoreUser(connectFormValues.adminId,connectFormValues.userId,connectFormValues.storeId)
  };

  return (
    <Center>
      <Card sx={{ width: 1000 }}>
        <CardContent sx={{ textAlign: 'center' }}>
          <Typography variant="h2" sx={{ my: 3 }}>
            Admin Page
          </Typography>

          <Button onClick={() => setShowAddUserForm(true)}>Add User</Button>
          <Button onClick={() => setShowAddStoreForm(true)}>Add Store</Button>
          <Button onClick={() => setShowConnectStoreUserForm(true)}>Connect Store and User</Button>

          {showAddUserForm && (
            <form onSubmit={handleSubmitUserForm}>
              {/* Render the fields of the user form using TextField components */}
              {/* Call handleUserFormInputChange for the onChange prop of the TextField components */}
              {/* Bind the TextField component values to the userFormValues */}
              <Button type="submit">Submit</Button>
            </form>
          )}

          {showAddStoreForm && (
            <form onSubmit={handleSubmitStoreForm}>
              {/* Render the fields of the store form using TextField components */}
              {/* Call handleStoreFormInputChange for the onChange prop of the TextField components */}
              {/* Bind the TextField component values to the storeFormValues */}
              <Button type="submit">Submit</Button>
            </form>
          )}

          {showConnectStoreUserForm && (
            <form onSubmit={handleSubmitConnectForm}>
              {/* Render the fields of the connect form using TextField components */}
              {/* Call handleConnectFormInputChange for the onChange prop of the TextField components */}
              {/* Bind the TextField component values to the connectFormValues */}
              <Button type="submit">Submit</Button>
            </form>
          )}
        </CardContent>
      </Card>
    </Center>
  );
}

