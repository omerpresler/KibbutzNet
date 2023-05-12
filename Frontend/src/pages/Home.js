import React, { useEffect } from 'react'
import { Button, Card, CardContent, TextField, Typography } from '@mui/material'
import { Box } from '@mui/system'
import Center from '../components/Center'
import {useNavigate} from 'react-router-dom'
import GetLoginService from '../services/loginService'
import * as paths from '../services/pathes';
import BackButton from '../components/BackButton';


export default function Home() {
    const navigate=useNavigate()
   

function move_register(){
    localStorage.setItem("nextPage",paths.register_page_path)
    navigate(paths.login_to_store)
}

function move_to_member(){
    navigate(paths.login_to_user)
}

function move_to_store(){
    localStorage.setItem("nextPage",paths.store_page_path)
    navigate(paths.login_to_store)
}


    return (
        <Center>
        <Card sx={{ width: 1000 }}>
            <CardContent sx={{ textAlign: 'center' }}>
                <Typography variant="h2" sx={{ my: 3 }}>
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