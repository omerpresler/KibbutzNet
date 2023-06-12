import React, { useState,useEffect } from 'react'
import { Button, Card, CardContent, TextField, Typography } from '@mui/material'
import { Box } from '@mui/system'
import { Switch, FormControlLabel } from '@mui/material';
import Center from '../components/Center'
import useForm from '../hooks/useFrom'
import {useNavigate} from 'react-router-dom'
import * as paths from '../services/pathes';
import BackButton from '../components/BackButton';
import GetLoginService from '../services/loginService'
const getLoginModel = () => ({
    email: '',
    accountNumberber: ''
})

export default function LoginUser() {
    const navigate=useNavigate()
    const [asAdmin, setasAdmin] = useState(false);
    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange
    } = useForm(getLoginModel);

    
    const { user, loginToUser,LoginToStore, logout, isAuthenticated }=GetLoginService();



    async function login(e){
        e.preventDefault();
        if (validate()){
            const user_type=await loginToUser(values.email,values.accountNumberber,asAdmin);
            if (user_type=="user"){
             navigate(paths.member_page_path)
            }
            if(user_type=="admin"){
                navigate(paths.admin_page_path)
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
            <Card sx={{ width: 1000 }}>
                <CardContent sx={{ textAlign: 'center' }}>
                    <Typography variant="h3" sx={{ my: 3,textAlign:'center' }}>
                        קיבוץ נט
                    </Typography>
                    <Box sx={{
                        '& .MuiTextField-root': {
                            m: 1,
                            width: '90%'
                        }
                    }}>
                        <form noValidate autoComplete="off" onSubmit={login}>
                            <TextField
                                label="איימל"
                                name="email"
                                value={values.email}
                                onChange={handleInputChange}
                                variant="outlined"
                                {...(errors.email && { error: true, helperText: errors.email })} />
                            <TextField
                                label="מספר חשבון "
                                name="accountNumberber"
                                value={values.accountNumberber}
                                onChange={handleInputChange}
                                variant="outlined"
                                {...(errors.accountNumberber && { error: true, helperText: errors.accountNumberber })} />
                                    <FormControlLabel
                                control={
                                    <Switch 
                                        checked={asAdmin} 
                                        onChange={() => setasAdmin(!asAdmin)}
                                        name="adminToggle"
                                        color="primary"
                                    />
                                }
                                label={asAdmin ? 'אדמין' : 'יוזר'}
                                sx={{ width: '90%', marginTop: '1rem' }}
                            />
                            <Button
                                type="login"
                                variant="contained"
                                size="large"
                                sx={{ width: '90%' }}>התחבר</Button>
                            
                        </form>
                        <BackButton sx={{ mt: 2 }} />
                    </Box>
                </CardContent>
            </Card>
        </Center>


    )
}