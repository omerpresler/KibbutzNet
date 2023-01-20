import { Fab } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import * as paths from './pathes';
import axios from 'axios'

const path=paths.back_path+paths.login_controller_path+"/login" 
export default function checkLoginDataInBackend(email,accountNum){
    axios.post(path, {
            
        })
    .then(function (response) {
        console.log(response)
        if (response.data===true){
            console.log("yes")
            localStorage.setItem('email',email);
            return true;
        }
        return false;
    })
    .catch(function (error) {
        console.log(error);
        return false;
    });
}