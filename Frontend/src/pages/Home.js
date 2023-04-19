import React, { useEffect } from 'react'
import { Button, Card, CardContent, TextField, Typography } from '@mui/material'
import { Box } from '@mui/system'
import Center from '../components/Center'
import useForm from '../hooks/useFrom'
import {useNavigate} from 'react-router-dom'
import GetLoginService from '../services/loginService'
import * as paths from '../services/pathes';
import BackButton from '../components/BackButton';


export default function Home() {
    const navigate=useNavigate()
   

function move_register(){
    navigate(paths.login_to_register)
}

function move_to_member(){
    navigate(paths.login_to_user)
}

function move_to_store(){
    navigate(paths.login_to_store)
}


    return (
        <Center>
        <Card sx={{ width: 1000 }}>
            <CardContent sx={{ textAlign: 'center' }}>
                <Typography variant="h1" sx={{ my: 3 }}>
                    kibbutzNet
                </Typography>
                <Box sx={{
                    '& .MuiTextField-root': {
                        m: 1,
                        width: '90%',
                        length: '80%'
                    }
                }}>
                <Button onClick={move_register}>
                                    Register</Button>
                <Button onClick={move_to_member}>
                                    kibutz member</Button>
                <Button onClick={move_to_store}>
                                    kibutz store</Button>
                </Box>
            </CardContent>
        </Card>
    </Center>
    )
}