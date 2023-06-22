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
      <Table sx={{ minWidth: 1500 }} aria-label="simple table">
        <TableHead>
          <TableRow>
          <TableCell>שם החנות</TableCell>
            <TableCell>מספר הזמנה</TableCell>
            <TableCell>תאריך</TableCell>
            <TableCell>סטטוס</TableCell>
            <TableCell>שם החבר </TableCell>
            <TableCell>מספר תקציב</TableCell>
            <TableCell onClick={handleSortByStatus} style={{cursor: 'pointer'}}>פעיל</TableCell>
            {/* <TableCell>Chat</TableCell> */}
            <TableCell>מחיר</TableCell>
            <TableCell>פירוט</TableCell>
            {localStorage.getItem("userType") === "store" && (
  <>
    <TableCell>שנה סטטוס</TableCell>
    <TableCell>Action</TableCell>
  </>
)}
          </TableRow>
        </TableHead>
        <TableBody>
          {orders.map((order) => (
            <TableRow key={order.orderId}  sx={{ backgroundColor: order.active ? 'lightgreen' : 'lightcoral' }}>
              <TableCell>{order.storeName}</TableCell>
              <TableCell component="th" scope="row">{order.orderId}</TableCell>
              <TableCell>{new Date(order.date).toLocaleDateString()}</TableCell>
              <TableCell>{order.status}</TableCell>
              <TableCell>{order.memberName}</TableCell>
              <TableCell>{order.memberId}</TableCell>
              <TableCell>{order.active ? 'כן' : 'לא'}</TableCell>
              {/* <TableCell>{"chat connection "}</TableCell> */}
              <TableCell>{order.cost}</TableCell>
              <TableCell>{order.description}</TableCell>
              {localStorage.getItem("userType") === "store" && (
      <TableCell>
        <Button
          onClick={() => handleOpenDialog(order.orderId)}
          variant="contained"
          color="primary"
          size="small" // size is added here
        >
          שנה סטטוס
        </Button>
      </TableCell>
    )}
    {localStorage.getItem("userType") === "store" && (
      <TableCell>
        <Button
          onClick={() => handleToggleOrderActive(order.orderId)}
          variant="contained"
          color={order.active ? "secondary" : "primary"}
          size="small" // size is added here
        >
          {order.active ? 'סגור' : 'פתח מחשב'}
        </Button>
      </TableCell>
    )}
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
            label="סטטוס חדש"
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
