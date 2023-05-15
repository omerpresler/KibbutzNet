import Center from "../components/Center";
import GetOrderService from "../services/OrderService";
import { Box, TextField, Button, Card, CardContent, Typography, Paper } from '@mui/material';
import React, { useState } from 'react';
import BackButton from '../components/BackButton';
import useForm from '../hooks/useFrom';
import OrderDisplyer from "../components/OrderDisplyer";

const OrderManager = () => {
  const { addOrder, changeOrdersStatus, getAllOrdersStore, getAllOrdersUser, closeOrder, reOpenOrder } = GetOrderService();
  const initialFormValues = () => ({
    storeId: '',
    userId: '',
  });
  const { values, setValues, errors, setErrors, handleInputChange } = useForm(initialFormValues);
  const [isLoading, setIsLoading] = useState(false);
  const [data, setData] = useState([]);
  const [isFormSubmitted, setIsFormSubmitted] = useState(false);
  const userType = localStorage.getItem("userType");

  const handleSubmit = async () => {
    setIsFormSubmitted(true);
    setIsLoading(true);
    try {
      let response;
      if (userType === 'store') {
        response = await getAllOrdersStore(values.userId);
      } else {
        response = await getAllOrdersUser(values.storeId);
      }
      console.log(response)
      console.log(response.value)
      setData(response.value);
    } catch (error) {
      setData(null);
      console.error(error);
    } finally {
      setIsLoading(false);
    }
  };

  const handleChangeStatus = async (orderId, statusMessage) => {
    await changeOrdersStatus(localStorage.getItem("storeId"), orderId, statusMessage)
  };

  const handleToggleOrderActive = async (orderId) => {
    let order = data.find(order => order.orderId === orderId);
    console.log(order)
    console.log(orderId)
    if (order.active) {
      await closeOrder(orderId);
    } else {
      await reOpenOrder(orderId);
    }
    // Update the local copy of the data
    order.active = !order.active;
    setData([...data]);
  };

  return (
    <Center>
      <Box>
        <Card>
          <CardContent>
            <Typography variant="h5" gutterBottom>
              Enter id
            </Typography>
            {userType !== 'store' && (
              <TextField
                name="storeId"
                label="Store ID"
                value={values.storeId}
                onChange={handleInputChange}
              />
            )}
            {userType !== "user" && (
              <TextField
                name="userId"
                label="User ID"
                value={values.userId}
                onChange={handleInputChange}
              />
            )}
            <Button onClick={handleSubmit}>Submit</Button>
          </CardContent>
        </Card>
      </Box>
      {isFormSubmitted && (
        <Center>
          <Paper elevation={3} style={{ padding: '1rem', maxWidth: '800px', width: '100%' }}>
            <Typography variant="h5" gutterBottom>
              Order History
            </Typography>
            {isLoading ? <div>Loading...</div> : <OrderDisplyer orders={data} handleChangeStatus={handleChangeStatus} handleToggleOrderActive={handleToggleOrderActive} />}
          </Paper>
          <BackButton sx={{ mt: 2 }} />
        </Center>
      )}
    </Center>
  );
};

export default OrderManager;
