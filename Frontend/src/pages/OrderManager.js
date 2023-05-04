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
import OrderDisplyer from "../components/OrderDisplyer";
import GetOrderService from "../services/OrderService";
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


export default function OrderManager() {
  const {addOrder,changeOrdersStatus,getAllOrdersStore,getAllOrdersUser} =GetOrderService() 
  const initialFormValues = () => ({
    storeId: '',
    userId: '',
})
  const {values, setValues,errors,setErrors,handleInputChange}=useForm(initialFormValues)
  const [isLoading, setIsLoading] = useState(false);
  const [data, setData] = useState([]);
  const [isFormSubmitted, setIsFormSubmitted] = useState(false);
  const userType=localStorage.getItem("userType")
  
  const handleSubmit = async () => {
    setIsFormSubmitted(true);
    setIsLoading(true);
    try {
      let response;
      if (userType == 'store') {
        response = await getAllOrdersStore(values.userId); 
      } else {
        response = await getAllOrdersUser(values.storeId); 
      }
      setData(response.value.map(JSON.parse));
    } catch (error) {
      setData(null);
      console.error(error);
    } finally {
      setIsLoading(false);
    }
  };

  function handleChangeStatus(){
  }
  return (
    <Center>
    <Box>
      <Card>
        <CardContent>
          <Typography variant="h5" gutterBottom>
            Enter id
          </Typography>
          {userType != 'store' && (
            <TextField
              name="storeId"
              label="Store ID"
              value={values.storeId}
              onChange={handleInputChange}
            />
          )}
          {userType != "user"&&(<TextField
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
            {isLoading ? <div>Loading...</div> : <OrderDisplyer orders={data} handleChangeStatus={handleChangeStatus} />}
          </Paper>
          <BackButton sx={{ mt: 2 }} />
        </Center>
      )}
  </Center>
  );
}
