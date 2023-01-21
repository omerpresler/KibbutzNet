import useForm from '../hooks/useFrom'
import Center from "../components/Center";
import { Button, Card, CardContent, TextField, Typography } from '@mui/material'
import axios from 'axios'
import GetRegsiterService from '../services/RegisterService';
import GetLoginService from '../services/loginService';

import {useNavigate} from 'react-router-dom'



const getPurchaseModel = () => ({
    price:'',
    description:'',
    accountNumber:''
 })
export default function Regsiter() {
    const navigate=useNavigate()
    const LoginService= GetLoginService()
    const {addNewPurhcase,seePurchaseHistory}=GetRegsiterService();
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange
    } = useForm(getPurchaseModel);
    return <Center>
        <TextField
                                label="price"
                                name="price"
                                value={values.price}
                                onChange={handleInputChange}
                                variant="outlined"
                                {...(errors.price && { error: true, helperText: errors.price })} /> 
        <TextField
                                label="description"
                                name="description"
                                value={values.description}
                                onChange={handleInputChange}
                                variant="outlined"
                                {...(errors.description && { error: true, helperText: errors.description })} /> 
        <TextField
                                label="accountNumber"
                                name="accountNumber"
                                value={values.accountNumber}
                                onChange={handleInputChange}
                                variant="outlined"
                                {...(errors.accountNumber && { error: true, helperText: errors.accountNumber })} /> 

        <Button    
         onClick={()=>addNewPurhcase(values.price,values.description,values.accountNumber)}   
            >
            add new purahcse

        </Button>
       
        <Button onClick={()=>seePurchaseHistory(0,1)}   >

            see purchase history

        </Button>
        <Button    N
         onClick={()=>(LoginService.logout())}   
            >
            logout

        </Button>
    </Center>
}