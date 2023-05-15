import axios from 'axios';
import * as paths from './pathes';
import { Response } from './Response';

export default function GetUserService() {

function getAllStores() {
    return axios
      .post(paths.getAllStores)
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage)
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured,response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error sending user message:', error);
        return Response.create([], true, error.message);
      });
  }
  return {getAllStores}
}
