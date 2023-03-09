import React, { useEffect } from 'react'
import { Button, Card, CardContent, TextField, Typography } from '@mui/material'
import { Box } from '@mui/system'
import Center from '../components/Center'
import useForm from '../hooks/useFrom'
import {useNavigate} from 'react-router-dom'
import GetLoginService from '../services/loginService'
import * as paths from '../services/pathes';

const getLoginModel = () => ({
    email: '',
    accountNumberber: ''
})

export default function Store() {
    const navigate=useNavigate()




    return (
        <h1>
        kibbutzStore
        </h1>
    )
}