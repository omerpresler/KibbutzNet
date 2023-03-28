import { Fab } from '@mui/material';
import { json, useNavigate } from 'react-router-dom';
import * as paths from './pathes';
import axios from 'axios'
import { useState } from 'react';
const LoginFunctionPath=paths.back_path+paths.login_controller_path+"/login" 

let accountNumberSaver = null;
let storeIdSaver = null;

export default function GetLoginService() {
  const navigate=useNavigate();
  const [user, setUser] = useState(accountNumberSaver);
  const [storeId, setStoreID] = useState(storeIdSaver);

  function loginToUser(email, accountNumber) {
    const ServerAns=sendLoginAsUser(email,accountNumber);
    // if(ServerAns===true){
      if (true){
    localStorage.clear();
    localStorage.setItem('email',email);
    localStorage.setItem('accountNumber',accountNumber)
    accountNumberSaver = { accountNumber };
    setUser(accountNumberSaver);
    }
  }

  function loginToStore(email, accountNumber,storeId) {
    const ServerAns=sendLoginAsStore(email,accountNumber,storeId);
    // if(ServerAns===true){
      if (true){
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

  return { user, loginToUser,loginToStore, logout, isAuthenticated };
}
 function sendLoginAsUser(email,accountNumber){
    console.log(accountNumber);
    axios.post(LoginFunctionPath, {
            email: JSON.stringify(email),
            accountNumber:JSON.stringify(accountNumber)
        })
    .then(function (response) {
        console.log(response)
        if (response.data===true){
 
        }
        return false;
    })
    .catch(function (error) {
        console.log(error);
        return false;
    });
  }

    function sendLoginAsStore(email,accountNumber,storeId){
      console.log(storeId);
      axios.post(LoginFunctionPath, {
              email: JSON.stringify(email),
              accountNumber:JSON.stringify(accountNumber),
              storeId:JSON.stringify(storeId)
          })
      .then(function (response) {
          if (response.data===true){
            localStorage.setItem('storeId',storeId);
          }
          return false;
      })
      .catch(function (error) {
          console.log(error);
          return false;
      });


}