import React, { useEffect } from 'react'
import { Button, Card, CardContent, TextField, Typography } from '@mui/material'
import { Box } from '@mui/system'
import Center from '../components/Center'
import useForm from '../hooks/useFrom'
import {useNavigate} from 'react-router-dom'
import * as paths from '../services/pathes';
import GetLoginStoreService from "../services/loginStoreService";

const getLoginStoreModel = () => ({
    store:'',
    password:'',
})

export default function LoginStore() {
    const navigate=useNavigate()
    
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange
    } = useForm(getLoginStoreModel);


    const {store, checkLoginStoreDataInBackend, logout, isAuthenticated } = GetLoginStoreService()

    const login = e => {
        e.preventDefault();
        if (validate()){
            const didLoginSucsed=checkLoginStoreDataInBackend(values.store,values.password,);
            if (isAuthenticated){
                navigate(paths.register_contorller_path)
            }
        }
    }

    const validate = () => {
        let temp = {}
        temp.store = values.store != "" ? (!isNaN(values.store) ? "Store has to be a number." : "") : "This field is required."
        temp.password = values.password != "" ? "" : "This field is required."
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
                                label="Store"
                                name="store"
                                value={values.store}
                                onChange={handleInputChange}
                                variant="outlined"
                                {...(errors.store && { error: true, helperText: errors.store })} />
                            <TextField
                                type="password"
                                label="password"
                                name="password"
                                value={values.password}
                                onChange={handleInputChange}
                                variant="outlined"
                                {...(errors.password && { error: true, helperText: errors.password })} />
                            <Button
                                type="loginStore"
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