import { Fab } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import * as paths from './pathes';
import axios from 'axios'
import { useState } from 'react';

const LoginFunctionPath=paths.back_path+paths.login_controller_path+"/login" 

let accountNumberber = null;

export default function GetLoginService() {
  const [user, setUser] = useState(accountNumberber);

  function checkLoginDataInBackend(email, accountNumber) {
    const ServerAns=sendLoginRequest(email,accountNumber);
    console.log(ServerAns);
    if(ServerAns===true){
    accountNumberber = { accountNumber };
    setUser(accountNumberber);
    }
  }

  function logout() {
    localStorage.clear();
    accountNumberber = null;
    setUser(accountNumberber);
  }

  function isAuthenticated() {
    return !!user;
  }

  return { user, checkLoginDataInBackend, logout, isAuthenticated };
}
 function sendLoginRequest(email,accountNumber){
    console.log(accountNumber);
    axios.post(LoginFunctionPath, {
            email: JSON.stringify(email),accountNumber:JSON.stringify(accountNumber)
        })
    .then(function (response) {
        console.log(response)
        if (response.data===true){
            console.log("yes")
            localStorage.clear();
            localStorage.setItem('email',email);
            localStorage.setItem('accountNumber',accountNumber)
            return 1;
        }
        return false;
    })
    .catch(function (error) {
        console.log(error);
        return false;
    });


}