import React, { useState } from 'react';
import GetReportService from '../services/RepertService';
import { Button, Card, CardContent, Typography, Dialog, DialogTitle, DialogContent, DialogContentText, TextField, DialogActions } from '@mui/material';
import Center from "../components/Center";
import { Box } from '@mui/system';
import BackButton from '../components/BackButton';
export default function ReportPage() {
  const [storeId, setStoreId] = localStorage.getItem("storeId");
  const [email, setEmail] = useState('');
  const [open, setOpen] = useState(false);
  const { sendReportByEmail, saveExcelReport } = GetReportService();

  const handleSendReport = async () => {
    const result = await sendReportByEmail(storeId, email);
    if (result.exceptionHasOccured) {
      alert('Error sending report:', result.errorMessage);
    } else {
      alert('Report sent!');
      setOpen(false);
    }
  };

  const handleSaveReport = async () => {
    const result = await saveExcelReport(storeId);
    if (result.exceptionHasOccured) {
      alert('Error saving report:', result.errorMessage);
    } else {
      alert('Report saved!');
      setOpen(false);
    }
  };

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <Center>
      <Card sx={{ width: 1000 }}>
        <CardContent sx={{ textAlign: 'center' }}>
          <Typography variant="h2" sx={{ my: 3 }}>
            kibbutzNet
          </Typography>
          <Box sx={{
            '& .MuiTextField-root': {
              m: 1,
              width: '90%',
              length: '80%'
            }
          }}>
            <Button onClick={handleClickOpen}>
              Send Report email
            </Button>
            <Button onClick={()=>alert("not working yet")}>
              Send Report sms
            </Button>
            <Button onClick={()=>alert("no sap client was found")}>
              connect to sap
            </Button>
            <Button onClick={handleSaveReport}>
              Save report file 
            </Button>
      <BackButton back/>
          </Box>
          <Dialog open={open} onClose={handleClose}>
            <DialogTitle>Enter your email</DialogTitle>
            <DialogContent>
              <DialogContentText>
                Please enter your email to send the report.
              </DialogContentText>
              <TextField
                autoFocus
                margin="dense"
                id="email"
                label="Email Address"
                type="email"
                fullWidth
                value={email}
                onChange={(event) => setEmail(event.target.value)}
              />
            </DialogContent>
            <DialogActions>
              <Button onClick={handleClose}>
                Cancel
              </Button>
              <Button onClick={handleSendReport}>
                Send
              </Button>
            </DialogActions>
          </Dialog>
        </CardContent>
      </Card>
    </Center>
  );
}
