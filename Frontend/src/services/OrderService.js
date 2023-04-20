// src/services/orderService.js
import { Response } from "./Response";
import axios from "axios";
export default function GetOrderService() {
const apiBaseURL = "https://your-backend-url.com/api/";
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



  function addOrder(storeID, memberID, memberName, description, cost) {
    return axios.post(apiBaseURL + "addOrder", { storeID, memberID, memberName, description, cost })
      .then(res => {
        const response = Response.create(res.data.value, res.data.wasExecption);
        return response;
      })
      .catch(res => {
        const response = Response.create(res.data.value, res.data.wasExecption);
        return response;
      });
  }
  
  function changeOrdersStatus(storeID, orderID, status) {
    return axios.post(apiBaseURL + "changeOrdersStatus", { storeID, orderID, status })
      .then(res => {
        const response = Response.create(res.data.value, res.data.wasExecption);
        return response;
      })
      .catch(res => {
        const response = Response.create(res.data.value, res.data.wasExecption);
        return response;
      });
  }
  
  function ordersByStoreID(storeID) {
    return axios.post(apiBaseURL + "ordersByStoreID/" + storeID)
      .then(res => {
        const response = Response.create(res.data.value, res.data.wasExecption);
        return response;
      })
      .catch(res => {
        const response = Response.create(res.data.value, res.data.wasExecption);
        return response;
      });
    
  }
  
  function getAllOrdersStore(storeID) {
    return Response.create(fakeData,false)
    return axios.post(apiBaseURL + "ordersByStoreID/" + storeID)
      .then(res => {
        const response = Response.create(res.data.value, res.data.wasExecption);
        return response;
      })
      .catch(res => {
        const response = Response.create(res.data.value, res.data.wasExecption);
        return response;
      });
  }
  
  function getAllOrdersuser(UserId) {
    return Response.create(fakeData,false)
    try {
      const filteredData = fakeData.filter((order) => order.memberId === UserId);
      const response = Response.create(filteredData, false);
      return response;
    } catch (error) {
      const response = Response.create(error, true);
      return response;
    }
  }
return {addOrder,changeOrdersStatus,ordersByStoreID,getAllOrdersStore,getAllOrdersuser}  
}