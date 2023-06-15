import Center from "../components/Center";
import axios from 'axios';
import GetRegsiterService from '../services/RegisterService';
import {useNavigate} from 'react-router-dom';
import { Box } from '@mui/system';
import * as paths from '../services/pathes';
import GetStoreService from '../services/storeService';
import React, { useState, useEffect } from 'react';
import BackButton from '../components/BackButton';
import useForm from '../hooks/useFrom';
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
    storeName: '',
    userName: '',
  });

  const {values, setValues, errors, setErrors, handleInputChange} = useForm(initialFormValues);
  const [data, setData] = useState([]);
  const [finishFetching,setisFinshed]=useState(false);
  const {addNewPurhcase, seePurchaseHistoryUser, seePurchaseHistoryAllStore, seePurchaseHistoryUserAndStore} = GetRegsiterService();
  const userType = localStorage.getItem("userType");
  const handleSubmit = (values) => {
    setIsFormSubmitted(true);
  };
  const [isFormSubmitted, setIsFormSubmitted] = useState(false);

  useEffect(() => {
  
    const fetchData = async () => {
      try {
        console.log("trying to fetch dats")
        let response;
        if (userType === 'store') {
          response = await seePurchaseHistoryAllStore(localStorage.getItem("storeId"));
        } else {
          response = await seePurchaseHistoryUser(localStorage.getItem("userId"));
        }
        console.log(response)
        if (Array.isArray(response.value)) {
          setData(response.value);
        }
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

  return (
    
    <Center>
      <Box>
        <Card>
          <CardContent>
            <Typography variant="h5" gutterBottom>
               שם
            </Typography>
            {userType !== 'store' && (
              <TextField
                name="storeName"
                label="Store Name"
                value={values.storeName}
                onChange={handleInputChange}
              />
            )}
            {userType !== 'user' && (
              <TextField
                name="userName"
                label="User Name"
                value={values.userName}
                onChange={handleInputChange}
              />
            )}
            <Button onClick={handleSubmit}>Submit</Button>
            <BackButton sx={{ mt: 2 }} />

      {finishFetching && (
          <Paper elevation={3} style={{padding: '2rem', maxWidth: '800px', width: '100%', marginBottom: '2rem'}}>
            <Typography variant="h5" gutterBottom style={{fontSize: '1.5rem'}}>
              Purchase History
            </Typography>
            <TableContainer component={Paper} style={{width: '100%'}}>
              <Table sx={{ minWidth: 1000 }} aria-label="simple table">
                <TableHead>
                  <TableRow>
                    <TableCell align="right">שם חנות</TableCell>
                    <TableCell align="right">מספר תקציב</TableCell>
                    <TableCell align="right">מספר קניה</TableCell>
                    <TableCell align="right">מחיר</TableCell>
                    <TableCell align="right">תיאור</TableCell>
                    <TableCell align="right">תאריך</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {data.length > 0 ? data.map((dataItem) => (
                    <TableRow key={dataItem.purchaseId}>

                      <TableCell align="right">
                        {dataItem.storeName}
                      </TableCell>
                      <TableCell align="right">
                        {dataItem.memberId}
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
      )}
                </CardContent>
        </Card>
      </Box>
    </Center>
  );
}
