import React, { useState } from 'react';
import GetReportService from '../services/ReportService';
import { Button, Card, CardContent, Typography, Dialog, DialogTitle, DialogContent, DialogContentText, TextField, DialogActions } from '@mui/material';
import Center from "../components/Center";
import { Box } from '@mui/system';
import BackButton from '../components/BackButton';

export default function ReportPage() {
  const storeId = localStorage.getItem("storeId");
  const [email, setEmail] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [openEmail, setOpenEmail] = useState(false);
  const [openSms, setOpenSms] = useState(false);
  const { sendReportByEmail, saveExcelReport, sendReoprtBySms } = GetReportService();

  const handleSendReportByEmail = async () => {
    const result = await sendReportByEmail(storeId, email);
    if (result.exceptionHasOccured) {
      alert('Error sending report:', result.errorMessage);
    } else {
      alert('Report sent!');
      setOpenEmail(false);
    }
  };

  const handleSaveReport = async () => {
    const result = await saveExcelReport(storeId);
    if (result.exceptionHasOccured) {
      alert('Error saving report:', result.errorMessage);
    } else {
      alert('Report saved!');
    }
  };

  const handleSendReportBySms = async () => {
    const result = await sendReoprtBySms(storeId, phoneNumber);
    if (result.exceptionHasOccured) {
      alert('Error saving report:', result.errorMessage);
    } else {
      alert('Report saved!');
      setOpenSms(false);
    }
  };

  const handleClickOpenEmail = () => {
    setOpenEmail(true);
  };

  const handleClickOpenSms = () => {
    setOpenSms(true);
  };

  const handleCloseEmail = () => {
    setOpenEmail(false);
  };

  const handleCloseSms = () => {
    setOpenSms(false);
  };

  return (
    <Center>
      <Card sx={{ width: 1000 }}>
        <CardContent sx={{ textAlign: 'center' }}>
          <Typography variant="h2" sx={{ my: 3,textAlign: 'center'}}>
            קיבוץ נט
          </Typography>
          <Box sx={{
            '& .MuiTextField-root': {
              m: 1,
              width: '90%',
              length: '80%'
            }
          }}>
            <Button onClick={handleClickOpenEmail}>
              שלח רפורט באיימל
            </Button>
            <Button onClick={handleClickOpenSms}>
              שלח רפורט בהודעה
            </Button>
            <Button onClick={()=>alert("no sap client was found")}>
              התחבר למערכת לניהול כספים
            </Button>
            <Button onClick={handleSaveReport}>
              שומר כקובץ 
            </Button>
          <BackButton back/>
          </Box>
          <Dialog open={openEmail} onClose={handleCloseEmail}>
            <DialogTitle>הכנס את האיימל</DialogTitle>
            <DialogContent>
              <DialogContentText>
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
              <Button onClick={handleCloseEmail}>
                Cancel
              </Button>
              <Button onClick={handleSendReportByEmail}>
                Send
              </Button>
            </DialogActions>
          </Dialog>
          <Dialog open={openSms} onClose={handleCloseSms}>
            <DialogTitle>Enter your phone number</DialogTitle>
            <DialogContent>
              <DialogContentText>
                Please enter your phone number to send the report.
              </DialogContentText>
              <TextField
                autoFocus
                margin="dense"
                id="phoneNumber"
                label="Phone Number"
                type="tel"
                fullWidth
                value={phoneNumber}
                onChange={(event) => setPhoneNumber(event.target.value)}
              />
            </DialogContent>
            <DialogActions>
              <Button onClick={handleCloseSms}>
                Cancel
              </Button>
              <Button onClick={handleSendReportBySms}>
                Send
              </Button>
            </DialogActions>
          </Dialog>
        </CardContent>
      </Card>
    </Center>
  );
}
