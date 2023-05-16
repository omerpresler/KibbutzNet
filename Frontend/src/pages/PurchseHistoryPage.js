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


export default function PurchaseHistoryDisplayer() {
  const initialFormValues = () => ({
    storeId: '',
    userId: '',
})

  const {values, setValues,errors,setErrors,handleInputChange}=useForm(initialFormValues)
  const [data, setData] = useState([]);
  const [finishFetching,setisFinshed]=useState(false)
  const [FormData, setFormData] = useState(0);
  const [isFormSubmitted, setIsFormSubmitted] = useState(false);
  const {addNewPurhcase,seePurchaseHistoryUser,seePurchaseHistoryAllStore,seePurchaseHistoryUserAndStore}= GetRegsiterService()
  const userType=localStorage.getItem("userType")
  const handleSubmit = (values) => {
    
    setIsFormSubmitted(true);

  };
  // ...

useEffect(() => {
  if (!isFormSubmitted) return;  // Do not execute if form is not submitted

  const fetchData = async () => {
    try {
      let response;
      if (userType === 'store') {
        response = await seePurchaseHistoryUserAndStore(localStorage.getItem("storeId"),values.userId); 
      } else {
        response = await seePurchaseHistoryUserAndStore(values.storeId,localStorage.getItem("userId"));
      }
      if (Array.isArray(response.value)) {
        setData(response.value.map(item => JSON.parse(item)));
      }
      console.log(  )
    } catch (error) {
      console.error(error);
    }
  };  
  fetchData();
}, [userType, values, isFormSubmitted]); 

useEffect(() => {
  if (data) {
      setisFinshed(true);
  }
}, [data]);



   

  useEffect(() => {
    if (data) {
        setisFinshed(true);
    }
}, [data]);

  return (
    <Center>
      <Box>
      <Card>
      <CardContent>     
      <Typography variant="h5" gutterBottom>
          Enter ID
        </Typography>
        {userType != 'store' && (
          <TextField
            name="storeId"
            label="Store ID"
            value={values.storeId}
            onChange={handleInputChange}
          />
        )}
        {userType!='user' &&(   <TextField
          name="userId"
          label="User ID"
          value={values.userId}
          onChange={handleInputChange}
        />)}
        <Button onClick={handleSubmit}>Submit</Button>
        <BackButton sx={{ mt: 2 }} />
        </CardContent>   
        </Card>    
          </Box>
      {finishFetching && (
        <Center>
          <Paper elevation={3} style={{padding: '2rem', maxWidth: '800px', width: '100%', marginBottom: '2rem'}}>
          <Typography variant="h5" gutterBottom style={{fontSize: '1.5rem'}}>
  Purchase History
</Typography>
            <TableContainer component={Paper} style={{width: '100%'}}>
              <Table sx={{ minWidth: 1000 }} aria-label="simple table">
              <TableHead>
  <TableRow>
    <TableCell>Member ID</TableCell>
    <TableCell align="right">Store ID</TableCell>
    <TableCell align="right">Purchase ID</TableCell>
    <TableCell align="right">Cost</TableCell>
    <TableCell align="right">Description</TableCell>
    <TableCell align="right">Date</TableCell>
  </TableRow>
</TableHead>
                <TableBody>
  {data.length > 0 ? data.map((dataItem) => (
    <TableRow key={dataItem.purchaseId}>
      <TableCell component="th" scope="row">
        {dataItem.memberId}
      </TableCell>
      <TableCell align="right">
        {dataItem.storeId}
      </TableCell>
      <TableCell align="right">
        {dataItem.purchaseId}
      </TableCell>
      <TableCell align="right">
        {dataItem.cost}
      </TableCell>
      <TableCell align="right">
        {dataItem.description}
      </TableCell>
      <TableCell align="right">
      {new Date(dataItem.date).toISOString().split('T')[0]}
      </TableCell>
    </TableRow>
  )) : (
    <TableRow>
      <TableCell colSpan={6}>No data available</TableCell>
    </TableRow>
  )}
</TableBody>

              </Table>
            </TableContainer>
          </Paper>
        </Center>
      )}
    </Center>
  );
}
