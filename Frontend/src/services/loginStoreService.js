import { Fab } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import * as paths from './pathes';
import axios from 'axios'
import { useState } from 'react';

const LoginStoreFunctionPath=paths.back_path+paths.store_login_controller_path+"/loginStore"

let store = null;

export default function GetLoginStoreService() {
    const navigate = useNavigate();
    const [storeID, setStore] = useState(store);

    function checkLoginStoreDataInBackend(storeID, password) {
        const ServerAns = sendLoginStoreRequest(storeID,password);
        console.log(ServerAns);
        if(ServerAns===true){
            store = { storeID };
            setStore(store);
        }
    }

    function logout() {
        localStorage.clear();
        store = null;
        setStore(store);
        navigate(paths.front_path)
    }

    function isAuthenticated() {
        return !!storeID;
    }

    return { storeID, checkLoginStoreDataInBackend, logout, isAuthenticated };
}
function sendLoginStoreRequest(storeID,password){
    console.log(storeID);
    axios.post(LoginStoreFunctionPath, {
        store: JSON.stringify(storeID),
        password:JSON.stringify(password)
    })
        .then(function (response) {
            console.log(response)
            if (response.data===true){
                console.log("yes")
                localStorage.clear();
                localStorage.setItem('store',storeID);
                localStorage.setItem('password',password)
                return 1;
            }
            return false;
        })
        .catch(function (error) {
            console.log(error);
            return false;
        });


}