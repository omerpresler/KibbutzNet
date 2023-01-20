import React, { useEffect } from 'react'
import { Button, Card, CardContent, TextField, Typography } from '@mui/material'
import { Box } from '@mui/system'
import Center from '../components/Center'
import useForm from '../hooks/useFrom'
import {useNavigate} from 'react-router-dom'
import checkLoginDataInBackend from '../services/loginService'
import * as paths from '../services/pathes';

const getFreshModel = () => ({
    email: '',
    accountNum: ''
})

export default function Login() {
    const navigate=useNavigate()


    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange
    } = useForm(getFreshModel);

    


    const login = e => {
        e.preventDefault();
        if (validate()){
            const didLoginSucsed=checkLoginDataInBackend(values.email,values.accountNum);
                console.log(didLoginSucsed)
            if (didLoginSucsed){
             navigate(paths.register_contorller_path)
            }
        }
    }

    const validate = () => {
        let temp = {}
        temp.email = (/\S+@\S+\.\S+/).test(values.email) ? "" : "Email is not valid."
        temp.accountNum = values.accountNum != "" ? "" : "This field is required."
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
                                name="accountNum"
                                value={values.accountNum}
                                onChange={handleInputChange}
                                variant="outlined"
                                {...(errors.accountNum && { error: true, helperText: errors.accountNum })} />
                            <Button
                                type="login"
                                variant="contained"
                                size="large"
                                sx={{ width: '90%' }}>Start</Button>
                        </form>
                    </Box>
                </CardContent>
            </Card>
        </Center>


    )
}