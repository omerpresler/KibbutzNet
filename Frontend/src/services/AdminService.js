// AdminService.js
import axios from 'axios';
import { Response } from './Response';
import * as paths from '../services/pathes';

export default function getAdminService() {
  function addNewUser(userId, name, phoneNumber, email) {
    const adminId=localStorage.getItem("userId")
    return axios
      .post(paths.addUser, {adminId,userId, name, phoneNumber, email })
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage)
        }else{
          alert("add user worked!!")
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured,response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error adding new user:', error);
        return Response.create(null, true, error.message);
      });
  };

  function addNewStore(storeId, storeName,photoLink) {
    const adminId=localStorage.getItem("userId")
    return axios
      .post(paths.addStore, {adminId,storeId,storeName,photoLink})
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage)
        } else{
          alert("add store sucssfully!!")
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured,response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error adding new store:', error);
        return Response.create(null, true, error.message);
      });
  };

  function connectStoreUser(userId, storeId) {
    const adminId=localStorage.getItem("userId")
    return axios
      .post(paths.connectStoreUser, { adminId, userId, storeId })
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage)
        }else{
          alert("connected store and user sucssfully!!")
        }
        
        return Response.create(response.data.value, response.data.exceptionHasOccured,response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error connecting store to user:', error);
        return Response.create(null, true, error.message);
      });
  };

  
  return {
    addNewUser,
    addNewStore,
    connectStoreUser
  };
}
