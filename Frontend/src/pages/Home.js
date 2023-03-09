import React, { useEffect } from 'react'
import { Button, Card, CardContent, TextField, Typography } from '@mui/material'
import { Box } from '@mui/system'
import Center from '../components/Center'
import useForm from '../hooks/useFrom'
import {useNavigate} from 'react-router-dom'
import GetLoginService from '../services/loginService'
import * as paths from '../services/pathes';


export default function Home() {
    const navigate=useNavigate()
   

function move_register(){
    navigate(paths.register_contorller_path)
}

function move_to_member(){
    navigate(paths.member_controller_path)
}

function move_to_store(){
    navigate(paths.store_controller_path)
}
    

    return (
      <Center>
          <Button onClick={move_register}>
                                 Register</Button>
            <Button onClick={move_to_member}>
                                 kibutz member</Button>
            <Button onClick={move_to_store}>
                                 kibutz store</Button>
                                </Center>
    )
}