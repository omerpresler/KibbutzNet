import React, { useState, useEffect } from 'react';
import { Box, TextField, Button, Card, CardContent, Typography, Paper } from '@mui/material';
import Center from "../components/Center";
import BackButton from '../components/BackButton';
import OrderDisplyer from "../components/OrderDisplyer";
import GetOrderService from "../services/OrderService";

const OrderManager = () => {
  const {addOrder,changeOrdersStatus,getAllOrdersStore,getAllOrdersUser, closeOrder,reOpenOrder}  = GetOrderService();
  const [orders, setOrders] = useState([]);
  const [filteredOrders, setFilteredOrders] = useState([]);
  const [filterString, setFilterString] = useState("");
  const userType = localStorage.getItem("userType");
  
  useEffect(() => {
    const fetchOrders = async () => {
      let response;
      if (userType === 'user') {
        response = await getAllOrdersUser();
      } else {
        response = await getAllOrdersStore();
      }

      if (response && response.value) {
        setOrders(response.value);
        setFilteredOrders(response.value);
      }
    };
    fetchOrders();
  }, [userType]);
  
  const handleFilter = () => {
    if (filterString) {
      const filtered = orders.filter(order => {
        if (userType === 'user') {
          return order.storeName === filterString;
        } else {
          return order.memberName === filterString;
        }
      });
      setFilteredOrders(filtered);
    } else {
      setFilteredOrders(orders);
    }
  };
  
  const handleChangeStatus = async (orderId, statusMessage) => {
    await changeOrdersStatus(localStorage.getItem("storeId"), orderId, statusMessage)
  };

  const handleToggleOrderActive = async (orderId) => {
    let order = orders.find(order => order.orderId === orderId);
    if (order.active) {
      await closeOrder(orderId);
    } else {
      await reOpenOrder(orderId);
    }
    // Update the local copy of the data
    order.active = !order.active;
    setOrders([...orders]);
    handleFilter();  // re-apply the current filter
  };

  return (
    <Center>
      <Box>
        <Card>
          <CardContent>
            <Typography variant="h5" gutterBottom>
              הכנס {userType === 'user' ? "שם חנות" : "שם משתמש"}  
            </Typography>
            <TextField
              name="filterString"
              label={userType === 'user' ? " שם חנות" : "שם חבר"}
              value={filterString}
              onChange={(e) => setFilterString(e.target.value)}
            />
            <Button onClick={handleFilter}>סנן</Button>
          </CardContent>
        <Paper elevation={3} style={{ padding: '1rem', maxWidth: '800px', width: '100%' }}>
          <Typography variant="h5" gutterBottom>
            :היסטוריית רכישות
          </Typography>
          <OrderDisplyer orders={filteredOrders} handleChangeStatus={handleChangeStatus} handleToggleOrderActive={handleToggleOrderActive} />
        </Paper>
        <BackButton sx={{ mt: 2 }} />
      </Card>
      </Box>
    </Center>
  );
};

export default OrderManager;
