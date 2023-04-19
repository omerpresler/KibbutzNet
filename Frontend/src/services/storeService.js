import { Fab } from '@mui/material';
import { json, useNavigate } from 'react-router-dom';
import * as paths from './pathes';
import axios from 'axios'
import { useState,useEffect } from 'react';
import { Response } from "./Response";
const getPurchseHistoryFunctionPath=paths.back_path+paths.login_controller_path+paths.get_purchase_path
export default function GetStoreService(){


async function getPurchaseHistory() {
    const serverAns = await getPurchaseHistoryFromServer();
    return serverAns
      
  }
function getPurchaseHistoryFromServer() {
  const fakeData = [
    {
      id: 1,
      Date: "2022-01-01",
      BudgetNumber: "123456",
      EmployeeID: "001",
      Cost: "$10.00",
      Description: "Office Supplies",
    },
    {
      id: 2,
      Date: "2022-02-01",
      BudgetNumber: "234567",
      EmployeeID: "002",
      Cost: "$25.00",
      Description: "Lunch Meeting",
    },
    {
      id: 3,
      Date: "2022-03-01",
      BudgetNumber: "345678",
      EmployeeID: "003",
      Cost: "$100.00",
      Description: "Team Building Activity",
    },
  ];
  const res= Response.create(fakeData, false); 
  console.log(res)
  // return res
    const storeid=localStorage.getItem('storeId')
    return axios.post(getPurchseHistoryFunctionPath, {
        storeId: JSON.stringify(storeid),
    })
      .then(res=> {
        const response = Response.create(res.data.value, res.data.wasExecption);
        return response;
      })
      .catch(res=> {
        const response = Response.create(res.data.value, res.data.wasExecption);
        return response;
      })
  };
  return {getPurchaseHistory}
}