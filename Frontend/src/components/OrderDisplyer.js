import React from 'react';
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Button
} from '@mui/material';

const OrderDisplyer = ({ orders, handleChangeStatus }) => {
        return (
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
                  <TableCell>Change Status</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {orders.map((order) => (
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
                    <TableCell>
                      <Button
                        onClick={() => handleChangeStatus(order.orderID)}
                        variant="contained"
                        color="primary"
                      >
                        Change Status
                      </Button>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        );
      };

export default OrderDisplyer;
