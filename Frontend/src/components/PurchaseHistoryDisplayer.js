import React from 'react';
import Center from './Center';
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
} from '@mui/material'
export default function PurchaseHistoryDisplayer(history) {
  console.log(history)
  const { dataList } = history;
    return (
        <Paper elevation={3} style={{ padding: '1rem', maxWidth: '800px', width: '100%' }}>
          <Typography variant="h5" gutterBottom>
            Purchase History
          </Typography>
          <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
              <TableHead>
                <TableRow>
                  <TableCell>Date</TableCell>
                  <TableCell align="right">מספר תקציב</TableCell>
                  <TableCell align="right">מספר חנות</TableCell>
                  <TableCell align="right">מספר עובד</TableCell>
                  <TableCell align="right">מחיר</TableCell>
                  <TableCell align="right">תיאור</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {dataList.map((data) => (
                  <TableRow key={data.PurchaseID}>
                    <TableCell component="th" scope="row">
                      {data.Date}
                    </TableCell>
                    <TableCell align="right">{data.BudgetNumber}</TableCell>
                    <TableCell align="right">{data.storeName}</TableCell>
                    <TableCell align="right">{data.EmployeeID}</TableCell>
                    <TableCell align="right">{data.Cost}</TableCell>
                    <TableCell align="right">{data.Description}</TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </Paper>
    );
  }
  
