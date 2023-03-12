import React, { useEffect } from 'react'
import { Button, Card, CardContent, TextField, Typography } from '@mui/material'
import { Box } from '@mui/system'
import Center from '../components/Center'
import useForm from '../hooks/useFrom'
import {useNavigate} from 'react-router-dom'
import GetLoginService from '../services/loginService'
import * as paths from '../services/pathes';
import GetRegsiterService from '../services/RegisterService';
import { useState }  from 'react';
import Form from '../components/Form'
import OpenProp from '../components/OpenProp'
import memberBackround from '../components/memberBackround'

export default function Member() {
    const {addNewPurhcase,seePurchaseHistory}=GetRegsiterService();
    const [formData, setFormData] = useState(null);
    const [open, setOpen] = useState(false);
    const handleClick = () => {
      setOpen(!open);
    };
    const handleSubmit = (data) => {
      setFormData(data);
      seePurchaseHistory(formData.start,formData.finish);
    };
  
    const fieldsForPurchaseHistory = [
        { name: 'start', label: 'start' },
        { name: 'finish', label: 'finish' },
      ];

    return (
       <Center>
          <Button onClick={handleClick}>
                                 see purhcase history</Button>
                                 {open && <Form fields={fieldsForPurchaseHistory} onSubmit={handleSubmit}/>}
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