// AdminService.js
import axios from 'axios';
import { Response } from './Response';
import * as paths from '../services/pathes';

export default function getAdminService() {
  function addNewUser(adminId, userId, name, phoneNumber, email) {
    return axios
      .post(paths.addUser, { adminId, userId, name, phoneNumber, email })
      .then((response) => {
        return Response.create(response.data.value, response.data.wasExecption);
      })
      .catch((error) => {
        console.log('Error adding new user:', error);
        return Response.create(null, true, error.message);
      });
  };

  function addNewStore(adminId, storeName) {
    return axios
      .post(paths.addStore, { adminId, storeName })
      .then((response) => {
        return Response.create(response.data.value, response.data.wasExecption);
      })
      .catch((error) => {
        console.log('Error adding new store:', error);
        return Response.create(null, true, error.message);
      });
  };

  function connectStoreUser(adminId, userId, storeId) {
    return axios
      .post(paths.connectStoreUser, { adminId, userId, storeId })
      .then((response) => {
        return Response.create(response.data.value, response.data.wasExecption);
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
