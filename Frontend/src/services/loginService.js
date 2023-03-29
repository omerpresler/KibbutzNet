import { Fab } from '@mui/material';
import { json, useNavigate } from 'react-router-dom';
import * as paths from './pathes';
import axios from 'axios'
import { useState,useEffect } from 'react';
import { Response } from "./Response";

const loginToUserFunctionPath=paths.back_path+paths.login_controller_path+paths.login_to_user 
const loginToStoreFunctionPath=paths.back_path+paths.login_controller_path+paths.login_to_store 

let accountNumberSaver = null;
let storeIdSaver = null;

export default function GetLoginService() {
  const navigate=useNavigate();
  const [user, setUser] = useState(accountNumberSaver);
  const [storeId, setStoreID] = useState(storeIdSaver);
  const [serverAns, setServerAns] = useState(undefined);

 

async function loginToUser(email, accountNumber) {
    const ServerAns=await sendLoginRequestAsUser(email,accountNumber);
    if(ServerAns===true){
    localStorage.clear();
    localStorage.setItem('email',email);
    localStorage.setItem('accountNumber',accountNumber)
    accountNumberSaver = { accountNumber };
    setUser(accountNumberSaver);
    }
  }

   async function LoginToStore(email, accountNumber,storeId) {
        const serverAns = await sendLoginRequestAsStore(email, accountNumber, storeId);
        if (serverAns.value=true){
        localStorage.clear();
        localStorage.setItem('email',email);
        localStorage.setItem('accountNumber',accountNumber)
        localStorage.setItem('storeId',storeId)
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
    return !!user ||storeId!=0;
  }

  return { user, loginToUser,loginToStore: LoginToStore, logout, isAuthenticated };

  function sendLoginRequestAsUser(email, accountNumber, ) {
    return axios.post(loginToUserFunctionPath, {
        email: JSON.stringify(email),
        accountNumber: JSON.stringify(accountNumber),
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
          email: JSON.stringify(email),
          accountNumber: JSON.stringify(accountNumber),
          storeId: JSON.stringify(storeId)
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

