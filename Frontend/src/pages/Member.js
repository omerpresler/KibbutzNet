import Center from "../components/Center";
import { Button, Card, CardContent, TextField, Typography } from '@mui/material'
import axios from 'axios'
import GetRegsiterService from '../services/RegisterService';
import GetLoginService from '../services/loginService';
import {useNavigate} from 'react-router-dom'
import { Box } from '@mui/system'
import * as paths from '../services/pathes';
import BackButton from '../components/BackButton';
import { useState } from 'react';
import useForm from '../hooks/useFrom';
import GetOrderService from "../services/OrderService";
export default function Store() {
const [showAddOrderForm, setShowAddOrderForm] = useState(false);
const {addOrder,changeOrdersStatus,ordersByStoreID,getAllOrdersStore,getAllOrdersuser}= GetOrderService()
const getOrderModel = () => ({
    storeId: '',
    memberId: '',
    memberName:'',
    description:'',
    cost:''
})
const {
    values,
    setValues,
    errors,
    setErrors,
    handleInputChange
} = useForm(getOrderModel);

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

// function see_all_pages(){
//     navigate(paths.page_manager_page_path)
// }
    
  return (
  <Center>
  <Card sx={{ width: 1000 }}>
      <CardContent sx={{ textAlign: 'center' }}>
          <Typography variant="h2" sx={{ my: 3, textAlign:'center', }}>
              קיבוץ נט
          </Typography>
          <Box sx={{
              '& .MuiTextField-root': {
                  m: 1,
                  width: '90%',
                  length: '80%'
              }
          }}>
          <Button onClick={see_purchse_history}>
                              היסטוריית קניות</Button>
           <Button onClick={open_chat_manager}>
                              שיחות</Button>
            <Button onClick={see_all_orders}>
                הזמנות 
                </Button>

                             <BackButton sx={{ mt: 2 }} /> 
          </Box>

                            </CardContent>
                        </Card>

                        </Center>)

                        }
