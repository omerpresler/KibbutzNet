import { Fab } from '@mui/material';
import { json, useNavigate } from 'react-router-dom';
import * as paths from './pathes';
import axios from 'axios'
import { useState,useEffect } from 'react';
import { Response } from "./Response";

const loginToUserFunctionPath=paths.back_path+paths.login_controller_path+paths.login_to_user 
const loginToStoreFunctionPath=paths.back_path+paths.login_controller_path+paths.login_to_store 
const loginToAdaminFunctionPath=paths.back_path+paths.login_controller_path+"/loginToAdmin" 

let accountNumberSaver = null;
let storeIdSaver = null;

export default function GetLoginService() {
  const navigate=useNavigate();
  const [user, setUser] = useState(accountNumberSaver);
  const [storeId, setStoreID] = useState(storeIdSaver);

 

async function loginToUser(email, accountNumber,asAdmin) {
    clearData()
    let ServerAns=null
    if (asAdmin){
    ServerAns=await sendLoginRequestAsAdamin(email,accountNumber);
    }else{
    ServerAns=await sendLoginRequestAsUser(email,accountNumber);
    }
    console.log(ServerAns)
    console.log(ServerAns.value)
    if (ServerAns.value=="user" || ServerAns.value=="admin"){
    localStorage.setItem('email',email);
    localStorage.setItem('userId',accountNumber)
    localStorage.setItem('userType','user');
    accountNumberSaver = { accountNumber };
    setUser(accountNumberSaver);
    }
    return ServerAns.value
  }

  function clearData(){
    const nextPage=localStorage.getItem("nextPage")
    setUser(null)
    setStoreID(-1)
    localStorage.clear()
    localStorage.setItem("nextPage",nextPage)
  }
   async function LoginToStore(email, accountNumber,storeId) {
        clearData()
        const serverAns = await sendLoginRequestAsStore(email, accountNumber, storeId);
        console.log(serverAns.value)
        if(serverAns.value==true){
        localStorage.setItem('email',email);
        localStorage.setItem('userId',accountNumber)
        localStorage.setItem('storeId',storeId)
        localStorage.setItem('userType','store');
        accountNumberSaver = { accountNumber };
        storeIdSaver = { storeId };
        setUser(accountNumberSaver);
        setStoreID(storeIdSaver);
   }
      return serverAns.value
  }

  async function LoginToRegister(email, accountNumber,storeId) {
    clearData()
    const serverAns = await sendLoginRequestAsStore(email, accountNumber, storeId);
    if (serverAns.value===true){
    localStorage.setItem('email',email);
    localStorage.setItem('accountNumber',accountNumber)
    localStorage.setItem('storeId',storeId)
    localStorage.setItem('userType','resgister');
    accountNumberSaver = { accountNumber };
    storeIdSaver = { storeId };
    setUser(accountNumberSaver);
    setStoreID(storeIdSaver);
  }
}
  
  function logout() {
    localStorage.clear();
    accountNumberSaver = null;
    storeId=-1;
    setUser(accountNumberSaver);
    setStoreID(storeId);
  }

  function isAuthenticated() {
    console.log(user!=null)
    return user!=null
  }

  function sendLoginRequestAsStore(email, accountNumber, storeId) {
    return axios.post(loginToStoreFunctionPath, {
        email: email,
        accountNumber: accountNumber,
        storeId: storeId
      })
      .then(res=> {
        const response = Response.create(res.data.value, res.data.exceptionHasOccured,res.data.errorMessage);
        if (res.data.exceptionHasOccured){
          alert(res.data.errorMessage)
        }
        return response;
      })
      .catch(res=> {
        const response = Response.create(res.data.value, res.data.exceptionHasOccured,res.data.errorMessage);;
        alert(res.data.exceptionHasOccured)
        return response;
      })
  };

  function sendLoginRequestAsUser(email, accountNumber ) {
    return axios.post(loginToUserFunctionPath, {
        email: email,
        accountNumber: accountNumber,
      })
      .then(res=> {
        const response = Response.create(res.data.value, res.data.exceptionHasOccured,res.data.errorMessage);
        if (res.data.exceptionHasOccured){
          alert(res.data.exceptionHasOccured)
        }
        return response;
      })
      .catch(res=> {
        const response = Response.create(res.data.value, res.data.exceptionHasOccured,res.data.errorMessage);
        alert(res.data.exceptionHasOccured)
        return response;
      })
  };

  
  function sendLoginRequestAsAdamin(email, accountNumber ) {
    return axios.post(loginToAdaminFunctionPath, {
        email: email,
        accountNumber: accountNumber,
      })
      .then(res=> {
        const response = Response.create(res.data.value, res.data.exceptionHasOccured,res.data.errorMessage);;
        if (res.data.exceptionHasOccured){
          alert(res.data.exceptionHasOccured)
        }
        return response;
      })
      .catch(res=> {
        const response = Response.create(res.data.value, res.data.exceptionHasOccured,res.data.errorMessage);;
        return response;
      })
  };
  return { user, loginToUser,loginToStore: LoginToStore, logout, isAuthenticated };
}

 

