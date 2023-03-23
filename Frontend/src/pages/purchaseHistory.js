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
import Form from '../components/Form'
import OpenProp from '../components/OpenProp'
import memberBackround from '../components/memberBackround'
import DataDisplay from '../components/DataDisplay'
import PurchaseHistoryDisplayer from '../components/PurchaseHistoryDisplayer'
export default function PurchseHistory() {
    const [formData, setFormData] = useState(null);
    const [sentForm,setSentForm]=useState(false)
    const {addNewPurhcase,seePurchaseHistory}=GetRegsiterService();

    const handleClick = (data) => {
      setFormData(data);
      setSentForm(true)
    };
    const fieldsForPurchaseHistory = [
        { name: 'form', label: 'form' },
        { name: 'to', label: 'to' },
        
      ];
      
    return (
       <Center>
                                 <Form fields={fieldsForPurchaseHistory} onSubmit={handleClick}/>
             {DataDisplay (()=>seePurchaseHistory(sentForm.from,sentForm.to),PurchaseHistoryDisplayer,sentForm)}
                                 </Center>

    )
}