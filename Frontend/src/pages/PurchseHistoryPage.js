import useForm from '../hooks/useFrom'
import Center from "../components/Center";
import { Button, Card, CardContent, TextField, Typography } from '@mui/material'
import axios from 'axios'
import GetRegsiterService from '../services/RegisterService';
import GetLoginService from '../services/loginService';
import {useNavigate} from 'react-router-dom'
import { Box } from '@mui/system'
import * as paths from '../services/pathes';
import GetStoreService from '../services/storeService';
import React, { useState, useEffect } from 'react';
import { List, ListItem, ListItemText } from '@mui/material';
import BackButton from '../components/BackButton';




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
                <Card sx={{ width: 1000 }}>
                <CardContent sx={{ textAlign: 'center' }}>
                    <Typography variant="h3" sx={{ my: 3 }}>
                        kibbutzNet
                    </Typography>
                    <Box sx={{
                        '& .MuiTextField-root': {
                            m: 1,
                            width: '90%'
                        }
                    }}>

      <List>
        {data.map((data) => (
          <ListItem key={data.PurchaseID}>
            <ListItemText primary={data.Date}/>
            <ListItemText primary={data.BudgetNumber}/>
            <ListItemText primary={data.EmployeeID}/>
            <ListItemText primary={data.Cost}/>
            <ListItemText primary={data.Description}/>
          </ListItem>
        ))}
      </List>
      <BackButton sx={{ mt: 2 }} />
      </Box>
                </CardContent>
            </Card>
    </Center>
  );
}
