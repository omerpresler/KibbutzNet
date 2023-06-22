import Center from "../components/Center";
import GetRegsiterService from '../services/RegisterService';
import { Box } from '@mui/system';
import React, { useState, useEffect } from 'react';
import BackButton from '../components/BackButton';
import useForm from '../hooks/useFrom';
import {
  TextField,
  Button,
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
    memberId: '',
  });

  const {values, handleInputChange} = useForm(initialFormValues);
  const [data, setData] = useState([]);
  const [finishFetching,setisFinshed]=useState(false);
  const { seePurchaseHistoryUser, seePurchaseHistoryAllStore } = GetRegsiterService();
  const userType = localStorage.getItem("userType");

  useEffect(() => {
    const fetchData = async () => {
      try {
        let response;
        if (userType === 'store') {
          response = await seePurchaseHistoryAllStore(localStorage.getItem("storeId"));
        } else {
          response = await seePurchaseHistoryUser(localStorage.getItem("userId"));
        }
        if (Array.isArray(response.value)) {
          setData(response.value);
        }
      } catch (error) {
        console.error(error);
      }
    };
    fetchData();
  }, [userType]);

  useEffect(() => {
    if (data) {
      setisFinshed(true);
    }
  }, [data]);

  const filterData = () => {
    let filteredData = [...data];
    if (values.storeName) {
      filteredData = filteredData.filter(item => item.storeName.includes(values.storeName));
    }
    if (values.memberId) {
      filteredData = filteredData.filter(item => String(item.memberId).includes(values.memberId));
    }
    return filteredData;
  };
  

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
                name="memberId"
                label="מספר תקציב"
                value={values.memberId}
                onChange={handleInputChange}
              />
            )}
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
                      {filterData().length > 0 ? filterData().map((dataItem) => (
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
