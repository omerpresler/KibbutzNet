import React, { useEffect } from 'react'
import { Button, Card, CardContent, TextField, Typography } from '@mui/material'
import { Box } from '@mui/system'
import Center from '../components/Center'
import useForm from '../hooks/useFrom'
import {Navigate, useNavigate} from 'react-router-dom'
import GetLoginService from '../services/loginService'
import * as paths from '../services/pathes';
import GetRegsiterService from '../services/RegisterService';
import { useState }  from 'react';


export default function Member() {
    const {addNewPurhcase,seePurchaseHistory}=GetRegsiterService();
    const [formData, setFormData] = useState(null);
    const [open, setOpen] = useState(false);
    const [getHistory,setGetHistory]=useState(false);
    const [history,setHistory]=useState(null);
    const Navigate=useNavigate()
    const goToSeePurhcaseHistoryPage = () => {
      Navigate(paths.purhcase_history)
    };
   
   
  


    return (
       <Center>
          <Button onClick={goToSeePurhcaseHistoryPage}>
                                 see purhcase history</Button>
            <Button onClick={4}>
                                start new chat</Button>
            <Button onClick={4}>
                                 see all active chats</Button>
            <Button onClick={4}>
                                 see archived chats</Button>
            <Button onClick={5}>
                                 settings</Button>
                                </Center>
    )
}