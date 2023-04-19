import React, { useEffect } from 'react'
import { Button, Card, CardContent, TextField, Typography } from '@mui/material'
import { Box } from '@mui/system'
import Center from '../components/Center'
import useForm from '../hooks/useFrom'
import {useNavigate} from 'react-router-dom'
import GetLoginService from '../services/loginService'
import * as paths from '../services/pathes';
import BackButton from '../components/BackButton';


const getLoginModel = () => ({
    email: '',
    accountNumberber: ''
})

export default function LoginStore(nextPage) {
    const navigate=useNavigate()


    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange
    } = useForm(getLoginModel);

    
    const { user,loginToUser,loginToStore, logout, isAuthenticated }=GetLoginService()
    const login = e => {
        e.preventDefault();
        if (validate()){
            const didLoginSucsed=loginToStore(values.email,values.accountNumber,values.storeId);
            if (isAuthenticated){
             navigate(nextPage.nextPage)
            }
        }
    }

    const validate = () => {
        let temp = {}
        temp.email = (/\S+@\S+\.\S+/).test(values.email) ? "" : "Email is not valid."
        temp.accountNumber = values.accountNumber != "" ? "" : "This field is required."
        temp.storeNumber = values.storeNumber != "" ? "" : "This field is required."
        setErrors(temp)
        return Object.values(temp).every(x => x == "")
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
                        <form noValidate autoComplete="off" onSubmit={login}>
                            <TextField
                                label="Email"
                                name="email"
                                value={values.email}
                                onChange={handleInputChange}
                                variant="outlined"
                                {...(errors.email && { error: true, helperText: errors.email })} />
                            <TextField
                                label="acount number "
                                name="accountNumber"
                                value={values.accountNumber}
                                onChange={handleInputChange}
                                variant="outlined"
                                {...(errors.accountNumber && { error: true, helperText: errors.accountNumber })} />
                            <TextField
                                label="store id "
                                name="storeId"
                                value={values.storeId}
                                onChange={handleInputChange}
                                variant="outlined"
                                {...(errors.storeNumber && { error: true, helperText: errors.storeId })} />
                            <Button
                                type="login"
                                variant="contained"
                                size="large"
                                sx={{ width: '90%' }}>Start</Button>
                        </form>
                        <BackButton sx={{ mt: 2 }} />
                    </Box>
                </CardContent>
            </Card>
        </Center>


    )
}