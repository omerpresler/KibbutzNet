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
function see_reports(){
    navigate(paths.report_page)
}
function show_add_order(){
    setShowAddOrderForm(true)
}
async function add_order(event){
    event.preventDefault();
    await addOrder(values.memberId,values.memberName,values.description,values.cost)
}

  return (
  <Center>
  <Card sx={{ width: 1000 }}>
      <CardContent sx={{ textAlign: 'center' }}>
          <Typography variant="h2" sx={{ my: 3 ,textAlign: 'center'}}>
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
                              מנהל הצאטים</Button>
            <Button onClick={see_all_orders}>
                מנהל הזמנה
                </Button>
            <Button onClick={show_add_order}>
                הוסף הזמנה 
                              </Button>
            <Button onClick={see_reports}>
                              דוחות</Button>
                              <BackButton sx={{ mt: 2 }} />
          </Box>
          {showAddOrderForm && (
  <Box>
                            <form noValidate autoComplete="off" onSubmit={add_order}>
                            
                        <TextField
                        label="memberId"
                        name="memberId"
                        value={values.memberId}
                        onChange={handleInputChange}
                        variant="outlined"
                        {...(errors.memberId && { error: true, helperText: errors.memberId })}
                        />
                        <TextField
                        label="member name"
                        name="memberName"
                        value={values.memberName}
                        onChange={handleInputChange}
                        variant="outlined"
                        {...(errors.memberName && { error: true, helperText: errors.memberName })}
                        />
                        <TextField
                        label="description"
                        name="description"
                        value={values.description}
                        onChange={handleInputChange}
                        variant="outlined"
                        {...(errors.description && { error: true, helperText: errors.description })}
                        />
                        <TextField
                        label="cost"
                        name="cost"
                        value={values.cost}
                        onChange={handleInputChange}
                        variant="outlined"
                        {...(errors.cost && { error: true, helperText: errors.cost })}
                        />
                        <Button
                        type="add Order"
                        variant="contained"
                        size="large"
                        sx={{ width: '90%' }}
                        onClick={add_order}
                        >
                        Add Order
                        </Button>

                                                </form>
                        </Box>
                        )}
                            </CardContent>
                        </Card>

                        </Center>)

                        }
