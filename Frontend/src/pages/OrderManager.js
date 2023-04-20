import Center from "../components/Center";
import axios from 'axios'
import GetRegsiterService from '../services/RegisterService';
import GetLoginService from '../services/loginService';
import {useNavigate} from 'react-router-dom'
import { Box } from '@mui/system'
import * as paths from '../services/pathes';
import GetStoreService from '../services/storeService';
import React, { useState, useEffect } from 'react';
import BackButton from '../components/BackButton';
import useForm from '../hooks/useFrom'
import { Response } from "../services/Response";
import {
  TextField,
  Button,
  List,
  ListItem,
  ListItemText,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Typography,
  Card,
  CardContent,
} from '@mui/material';
import GetOrderService from "../services/OrderService";


export default function OrderManager() {
  const {addOrder,changeOrdersStatus,ordersByStoreID,getAllOrdersStore,getAllOrdersuser} =GetOrderService() 
  const initialFormValues = () => ({
    storeId: '',
    userId: '',
})
  const {values, setValues,errors,setErrors,handleInputChange}=useForm(initialFormValues)
  const [isLoading, setIsLoading] = useState(true);
  const [data, setData] = useState([]);
  const [FormData, setFormData] = useState([]);
  const [isFormSubmitted, setIsFormSubmitted] = useState(false);

  const userType=localStorage.getItem("userType")
  const handleSubmit = (values) => {
    console.log("handle submit")
    setIsFormSubmitted(true);
  };

  useEffect(() => {
    console.log(localStorage.getItem("storeid")
    )
    console.log("useEffect")
    const fetchData = async () => {
      try {
        let response;
        if (userType === 'store') {
          response = await getAllOrdersStore(localStorage.getItem("storeId")); 
        } else {
          response = await getAllOrdersuser(localStorage.getItem("userId")); 
        }
        setData(response.value);
      } catch (error) {
        setData(null);
        console.error(error);
      } finally {
        setIsLoading(false);
      }
    };  
    fetchData();
  }, [userType,isFormSubmitted]); 

  if (isLoading) {
    return <div>Loading...</div>;
  }




  return (
    <Center>
    <Box>
      <Card>
        <CardContent>
          <Typography variant="h5" gutterBottom>
            Enter Store and User ID
          </Typography>
          {userType === 'store' && (
            <TextField
              name="storeId"
              label="Store ID"
              value={values.storeId}
              onChange={handleInputChange}
            />
          )}
          <TextField
            name="userId"
            label="User ID"
            value={values.userId}
            onChange={handleInputChange}
          />
  
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
          <TableContainer component={Paper}>
            <Table sx={{ minWidth: 1000 }} aria-label="simple table">
              <TableHead>
                <TableRow>
                  <TableCell>Order ID</TableCell>
                  <TableCell>Date</TableCell>
                  <TableCell>Status</TableCell>
                  <TableCell>Member Name</TableCell>
                  <TableCell>Member ID</TableCell>
                  <TableCell>Active</TableCell>
                  <TableCell>Chat</TableCell>
                  <TableCell>Cost</TableCell>
                  <TableCell>Description</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {data.map((order) => (
                  <TableRow key={order.orderID}>
                    <TableCell component="th" scope="row">
                      {order.orderID}
                    </TableCell>
                    <TableCell>{order.date}</TableCell>
                    <TableCell>{order.status}</TableCell>
                    <TableCell>{order.memberName}</TableCell>
                    <TableCell>{order.memberId}</TableCell>
                    <TableCell>{order.active ? 'Yes' : 'No'}</TableCell>
                    <TableCell>{"chat connection "}</TableCell>
                    <TableCell>{order.cost}</TableCell>
                    <TableCell>{order.description}</TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </Paper>
        <BackButton sx={{ mt: 2 }} />
      </Center>
    )}
  </Center>
  );  
}
