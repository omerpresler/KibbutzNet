import useForm from '../hooks/useFrom'
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

import {
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
} from '@mui/material';

export default function PurchaseHistoryDisplayer() {
  const [isLoading, setIsLoading] = useState(true);
  const [data, setData] = useState([]);

  const { getPurchaseHistory } = GetStoreService();

  useEffect(() => {
    getPurchaseHistory()
      .then(response => {
        setData(response.value);
        setIsLoading(false);
      })
      .catch(error => {
        console.error(error);
        setIsLoading(false);
      });
  }, []);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  return (
    <Center>
      <Paper elevation={3} style={{ padding: '1rem', maxWidth: '800px', width: '100%' }}>
        <Typography variant="h5" gutterBottom>
          Purchase History
        </Typography>
        <TableContainer component={Paper}>
          <Table sx={{ minWidth: 1000 }} aria-label="simple table">
            <TableHead>
              <TableRow>
                <TableCell>Date</TableCell>
                <TableCell align="right">Budget Number</TableCell>
                <TableCell align="right">Employee ID</TableCell>
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
                  <TableCell align="right">{data.BudgetNumber}</TableCell>
                  <TableCell align="right">{data.EmployeeID}</TableCell>
                  <TableCell align="right">{data.Cost}</TableCell>
                  <TableCell align="right">{data.Description}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Paper>
      <BackButton sx={{ mt: 2 }} />
    </Center>
  );
}
