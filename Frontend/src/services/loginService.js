import { Fab } from '@mui/material';
import { json, useNavigate } from 'react-router-dom';
import * as paths from './pathes';
import axios from 'axios'
import { useState,useEffect } from 'react';
import { Response } from "./Response";

const loginToUserFunctionPath=paths.back_path+paths.login_controller_path+paths.login_to_user 
const loginToStoreFunctionPath=paths.back_path+paths.login_controller_path+paths.login_to_store 





export default function GetLoginService() {
  const navigate=useNavigate();
  const [user, setUser] = useState(null);
  const [storeId, setStoreID] = useState(-1);
  const [serverAns, setServerAns] = useState(undefined);

 

async function loginToUser(email, accountNumber) {
    setStoreId(-1)
    setUser(null)
    const ServerAns=await sendLoginRequestAsUser(email,accountNumber);
    if (ServerAns)
    localStorage.clear();
    localStorage.setItem('email',email);
    localStorage.setItem('userId',accountNumber)
    localStorage.setItem('userType','user');
    accountNumberSaver = { accountNumber };
    setUser(accountNumberSaver);
  }

   async function LoginToStore(email, accountNumber,storeId) {
        setStoreId(-1)
        setUser(null)
        const serverAns = await sendLoginRequestAsStore(email, accountNumber, storeId);
        if(serverAns.value){
        localStorage.clear();
        localStorage.setItem('email',email);
        localStorage.setItem('userId',accountNumber)
        localStorage.setItem('storeId',storeId)
        localStorage.setItem('userType','store');
        accountNumberSaver = { accountNumber };
        storeIdSaver = { storeId };
        setUser(accountNumberSaver);
        setStoreID(storeIdSaver);
   }
  }

  async function LoginToRegister(email, accountNumber,storeId) {
    setStoreId(-1)
    setUser(null)
    const serverAns = await sendLoginRequestAsStore(email, accountNumber, storeId);
    if (serverAns.value){
    localStorage.clear();
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
    storeId=null;
    setUser(accountNumberSaver);
    setStoreID(storeId);
    navigate(paths.front_path)
  }

  function isAuthenticated() {
    return !!user ||storeId!=-1;
  }

  return { user, loginToUser,loginToStore: LoginToStore, logout, isAuthenticated };

  function sendLoginRequestAsUser(email, accountNumber ) {
    return axios.post(loginToUserFunctionPath, {
        email: email,
        accountNumber: accountNumber,
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
}

  function sendLoginRequestAsStore(email, accountNumber, storeId) {
      return axios.post(loginToStoreFunctionPath, {
          email: email,
          accountNumber: accountNumber,
          storeId: storeId
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

