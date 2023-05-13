import React, { useState } from 'react';
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Button,
  TextField,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions
} from '@mui/material';

const OrderDisplyer = ({ orders, handleChangeStatus,handleToggleOrderActive }) => {
  const [statusInput, setStatusInput] = useState('');
  const [open, setOpen] = useState(false);
  const [selectedOrderId, setSelectedOrderId] = useState(null);
  const [sortDirection, setSortDirection] = useState('asc');

  const handleStatusInputChange = (event) => {
    setStatusInput(event.target.value);
  };

  const handleOpenDialog = (orderId) => {
    setSelectedOrderId(orderId);
    setOpen(true);
  };

  const handleCloseDialog = () => {
    setOpen(false);
    setStatusInput('');
    setSelectedOrderId(null);
  };

  const handleSaveStatusChange = () => {
    handleChangeStatus(selectedOrderId, statusInput);
    handleCloseDialog();
  };

  const handleSortByStatus = () => {
    setSortDirection(sortDirection === 'asc' ? 'desc' : 'asc');
    orders.sort((a, b) => {
      if (a.active && !b.active) return sortDirection === 'asc' ? -1 : 1;
      if (!a.active && b.active) return sortDirection === 'asc' ? 1 : -1;
      return 0;
    });
  };
  

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
            <TableCell onClick={handleSortByStatus} style={{cursor: 'pointer'}}>Active</TableCell>
            <TableCell>Chat</TableCell>
            <TableCell>Cost</TableCell>
            <TableCell>Description</TableCell>
            <TableCell>Change Status</TableCell>
            <TableCell>Action</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {orders.map((order) => (
            <TableRow key={order.orderID}>
              <TableCell component="th" scope="row">
                {order.orderID}
              </TableCell>
              <TableCell>{new Date(order.date).toLocaleDateString()}</TableCell>
              <TableCell>{order.status}</TableCell>
              <TableCell>{order.memberName}</TableCell>
              <TableCell>{order.memberId}</TableCell>
              <TableCell>{order.active ? 'Yes' : 'No'}</TableCell>
              <TableCell>{"chat connection "}</TableCell>
              <TableCell>{order.cost}</TableCell>
              <TableCell>{order.description}</TableCell>
              <TableCell>
                <Button
                  onClick={() => handleOpenDialog(order.orderID)}
                  variant="contained"
                  color="primary"
                >
                  Change Status
                </Button>
              </TableCell>
              <TableCell>
                <Button
                  onClick={() => handleToggleOrderActive(order.orderID)}
                  variant="contained"
                  color={order.active ? "secondary" : "primary"}
                >
                  {order.active ? 'Close' : 'Reopen'}
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>

      <Dialog open={open} onClose={handleCloseDialog}>
        <DialogTitle>Change Status</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            id="status-input"
            label="New Status"
            type="text"
            value={statusInput}
            onChange={handleStatusInputChange}
            fullWidth
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseDialog}>Cancel</Button>
          <Button onClick={handleSaveStatusChange}>Save</Button>
        </DialogActions>
      </Dialog>

    </TableContainer>
  );
};

export default OrderDisplyer;
