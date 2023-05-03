import useForm from '../hooks/useFrom'
import Center from "../components/Center";
import { Button, Card, CardContent, TextField, Typography } from '@mui/material'
import axios from 'axios'
import GetRegsiterService from '../services/RegisterService';
import GetLoginService from '../services/loginService';
import {useNavigate} from 'react-router-dom'
import { Box } from '@mui/system'
import * as paths from '../services/pathes';
import BackButton from '../components/BackButton';
export default function Store() {
  const navigate=useNavigate()
function see_purchse_history(){
    navigate(paths.purchse_history_page_path)
}
  function open_chat_manager(){
    navigate(paths.chat_manager_page_path)
}

function see_all_orders(){
    navigate(paths.order_manager_page_path)
}
function see_all_pages(){
    navigate(paths.page_manager_page_path)
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
            <Button onClick={see_purchse_history}>
                                see purchse history</Button>
             <Button onClick={open_chat_manager}>
                                open chat manager</Button>
              <Button onClick={see_all_orders}>
                  see all orders 
                                </Button>
              <Button onClick={see_all_pages}>
                  see all pages 
                                </Button>
                                <BackButton sx={{ mt: 2 }} />
            </Box>
        </CardContent>
    </Card>
  </Center>)

}
