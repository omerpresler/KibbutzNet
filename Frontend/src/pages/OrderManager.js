import React from 'react';
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

export default function OrderManagerDisplayer({ orders }) {
  return (
    <Paper elevation={3} style={{ padding: '1rem', maxWidth: '800px', width: '100%' }}>
      <Typography variant="h5" gutterBottom>
        Order Manager
      </Typography>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>Member Name</TableCell>
              <TableCell align="right">Member ID</TableCell>
              <TableCell align="right">Cost</TableCell>
              <TableCell align="right">Description</TableCell>
              <TableCell align="right">Status</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {orders.map((order) => (
              <TableRow key={order.orderID}>
                <TableCell component="th" scope="row">
                  {order.memberName}
                </TableCell>
                <TableCell align="right">{order.memberId}</TableCell>
                <TableCell align="right">{order.cost}</TableCell>
                <TableCell align="right">{order.description}</TableCell>
                <TableCell align="right">{order.status}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Paper>
  );
}
