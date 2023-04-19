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

export default function LoginUser() {
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
            const didLoginSucsed=loginToUser(values.email,values.accountNumberber,);
            if (isAuthenticated){
             navigate()
            }
        }
    }

    const validate = () => {
        let temp = {}
        temp.email = (/\S+@\S+\.\S+/).test(values.email) ? "" : "Email is not valid."
        temp.accountNumberber = values.accountNumberber != "" ? "" : "This field is required."
        setErrors(temp)
        return Object.values(temp).every(x => x == "")
    }

    return (
        <Center>
            <Card sx={{ width: 400 }}>
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
                                name="accountNumberber"
                                value={values.accountNumberber}
                                onChange={handleInputChange}
                                variant="outlined"
                                {...(errors.accountNumberber && { error: true, helperText: errors.accountNumberber })} />
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