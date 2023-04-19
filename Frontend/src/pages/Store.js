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
          <Button onClick={see_purchse_history}>
                              see purchse history</Button>
                              <BackButton sx={{ mt: 2 }} />
          </Box>
      </CardContent>
  </Card>

</Center>)

}
