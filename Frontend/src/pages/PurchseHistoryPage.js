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
  const [isLoading, setIsLoading] = useState(true);
  const [data, setData] = useState([]);
  const [FormData, setFormData] = useState([]);
  const [isFormSubmitted, setIsFormSubmitted] = useState(false);
  const {addNewPurhcase,seePurchaseHistoryUser,seePurchaseHistoryAllStore,seePurchaseHistoryUserAndStore}= GetRegsiterService()
  const userType=localStorage.getItem("userType")
  const handleSubmit = (values) => {
    console.log("handle submit")
    setIsFormSubmitted(true);
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        let response;
        if (userType === 'store') {
          response = await seePurchaseHistoryAllStore(localStorage.getItem("storeId")); 
        } else {
          response = await seePurchaseHistoryUser(localStorage.getItem("userId")); 
        }
        setData(response.value.map(JSON.parse));;
      } catch (error) {
        setData(null);
        console.error(error);
      } finally {
        console.log(data)
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
      {isFormSubmitted && (
        <Center>
          <Paper elevation={3} style={{padding: '2rem', maxWidth: '800px', width: '100%', marginBottom: '2rem'}}>
          <Typography variant="h5" gutterBottom style={{fontSize: '1.5rem'}}>
  Purchase History
</Typography>
            <TableContainer component={Paper} style={{width: '100%'}}>
              <Table sx={{ minWidth: 1000 }} aria-label="simple table">
                <TableHead>
                  <TableRow>
                    <TableCell>Date</TableCell>
                    <TableCell align="right">member Id</TableCell>
                    <TableCell align="right">Cost</TableCell>
                    <TableCell align="right">Description</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {data.map((data) => (
                    <TableRow key={data.PurchaseID}>
                      <TableCell component="th" scope="row">
                        {data.Date}
                      </TableCell>
                      <TableCell align="right">{data.memberId}</TableCell>
                      <TableCell align="right">{data.Cost}</TableCell>
                      <TableCell align="right">{data.Description}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </Paper>
        </Center>
      )}
    </Center>
  );
}
