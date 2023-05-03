import axios from 'axios';
import * as paths from './pathes';
import { Response } from './Response';

export default function GetRegsiterService() {
  const sendRequest = async (path, data) => {
    try {
      const res = await axios.post(path, data);
      const response = Response.create(res.data.value, res.data.wasExecption);
      return response;
    } catch (error) {
      const response = Response.create(error.response.data.value, error.response.data.wasExecption);
      return response;
    }
  };

  const addNewPurhcase = (price, description, accountNumber) => {
    const storeId=localStorage.getItem("storeId")
    console.log(storeId)
    return sendRequest(paths.addPurchasePath, {
      accountNumber: accountNumber,
      storeId:storeId,
      price: price,
      description: description,
     
    });
  };

  const seePurchaseHistoryUser = (userId) => {
    return sendRequest(paths.seePurchaseHistoryUserPath, {
      UserId: userId,
    });
  };

  const seePurchaseHistoryStore = (storeId) => {
    return sendRequest(paths.seePurchaseHistoryStorePath, {
      StoreId: storeId,
    });
  };

  const seePurchaseHistoryAllStore = (storeId) => {
    return sendRequest(paths.seePurchaseHistoryStorePath, {
      StoreId: storeId,
    });
  };

  const seePurchaseHistoryUserAndStore = (storeId, userId) => {
    return sendRequest(paths.seePurchaseHistoryUserAndStorePath, {
      StoreId: storeId,
      UserId: userId,
    });
  };

  return {
    addNewPurhcase,
    seePurchaseHistoryUser,
    seePurchaseHistoryAllStore,
    seePurchaseHistoryUserAndStore,
  };
}
