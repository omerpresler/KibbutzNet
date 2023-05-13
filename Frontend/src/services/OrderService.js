// src/services/orderService.js
import { Response } from "./Response";
import axios from "axios";
import * as paths from './pathes';

export default function GetOrderService() {
const apiBaseURL=""
const fakeData = [
    {
      orderID: 1,
      date: "2023-04-10",
      status: "Processing",
      memberName: "John Doe",
      memberId: 101,
      active: true,
      Chat: {/* Chat properties */},
      cost: 150.5,
      description: "Electronics purchase",
    },
    {
      orderID: 2,
      date: "2023-04-15",
      status: "Shipped",
      memberName: "Jane Smith",
      memberId: 102,
      active: true,
      Chat: {/* Chat properties */},
      cost: 75.2,
      description: "Books purchase",
    },
    {
      orderID: 3,
      date: "2023-04-18",
      status: "Delivered",
      memberName: "Michael Johnson",
      memberId: 103,
      active: false,
      Chat: {/* Chat properties */},
      cost: 200.3,
      description: "Furniture purchase",
    },
  ];



  async function addOrder( memberID, memberName, description, cost) {
    console.log("add order")
    return axios.post(paths.addOrderPath, 
      { storeid:localStorage.getItem("storeId"),memberID: memberID,memberName: memberName,
      description: description, cost:cost })
      .then(res => {
        const response = Response.create(res.data.value, res.data.wasExecption);
        return response;
      })
      .catch(res => {
        const response = Response.create(res.data.value, res.data.wasExecption);
        return response;
      });
  }
  
  async function changeOrdersStatus(storeID, orderID, status) {
    return axios.post(paths.changeOrderStatus, { storeId:storeID, orderId:orderID, status:status })
      .then(res => {
        const response = Response.create(res.data.value, res.data.wasExecption);
        return response;
      })
      .catch(res => {
        const response = Response.create(res.data.value, res.data.wasExecption);
        return response;
      });
  }
  
  
  async function getAllOrdersStore(userId) {
    const storeId=localStorage.getItem("storeId")
    return axios.post( paths.getAllOrderStoreUser,{userId:userId,storeId:storeId}  )
      .then(res => {
        const response = Response.create(res.data.value, res.data.wasExecption);
        console.log(response)
        return response;
      })
      .catch(res => {
        const response = Response.create([], res.data.wasExecption);
        return response;
      });
  }
  
  async function getAllOrdersUser(StoreId) {
    const userId=localStorage.getItem("userId")
    return axios.post(paths.getAllOrderStoreUser,{ userId:userId,StoreId: StoreId})
    .then(res => {
      const response = Response.create(res.data.value, res.data.wasExecption);
      return response;
    })
    .catch(res => {
      const response = Response.create([], res.data.wasExecption);
      return response;
    });
  }
return {addOrder,changeOrdersStatus,getAllOrdersStore,getAllOrdersUser}  
}