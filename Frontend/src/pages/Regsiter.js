import useForm from '../hooks/useFrom'
import Center from "../components/Center";
import { Button, Card, CardContent, TextField, Typography } from '@mui/material'
import axios from 'axios'
import GetRegsiterService from '../services/RegisterService';
import GetLoginService from '../services/loginService';
import { Box } from '@mui/system'
import {useNavigate} from 'react-router-dom'
import BackButton from '../components/BackButton';
import * as paths from '../services/pathes';


const getPurchaseModel = () => ({
    price:'',
    description:'',
    accountNumber:''
 })
export default function Regsiter() {
    const navigate=useNavigate()
    function seePurchaseHistory(){
    navigate(paths.purchse_history_page_path)
}

    const {addNewPurhcase}=GetRegsiterService()
    const { user, loginToUser,loginToStore, logout, isAuthenticated }= GetLoginService()
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange
    } = useForm(getPurchaseModel);
    return(
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
        <TextField
                                label="מחיר"
                                name="price"
                                value={values.price}
                                onChange={handleInputChange}
                                variant="outlined"
                                {...(errors.price && { error: true, helperText: errors.price })} /> 
        <TextField
                                label="תיאור"
                                name="description"
                                value={values.description}
                                onChange={handleInputChange}
                                variant="outlined"
                                {...(errors.description && { error: true, helperText: errors.description })} /> 
        <TextField
                                label="מספר חשבון"
                                name="accountNumber"
                                value={values.accountNumber}
                                onChange={handleInputChange}
                                variant="outlined"
                                {...(errors.accountNumber && { error: true, helperText: errors.accountNumber })} /> 

        <Button    
         onClick={()=>addNewPurhcase(values.price,values.description,values.accountNumber)}   
            >
            הוסף קניה חדשה

        </Button>
       
        <Button onClick={(seePurchaseHistory)}   >

            היסטוריית רכישות
        </Button>
        {/* <Button    N
         onClick={()=>(LoginService.logout())}   
            >
            logout

        </Button> */}
         <BackButton sx={{ mt: 2 }} />
        </Box>
            </CardContent>
        </Card>
    </Center>
    )
}