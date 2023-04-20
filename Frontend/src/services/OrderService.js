import axios from 'axios';

const baseURL = 'your_base_API_URL_here';

const addOrder = async (storeID, memberID, memberName, description, cost) => {
  try {
    const response = await axios.post(`${baseURL}/addOrder`, {
      storeID,
      memberID,
      memberName,
      description,
      cost,
    });
    return response.data;
  } catch (error) {
    console.error('Error adding order:', error);
    return { message: error.message, exception: true };
  }
};

const changeOrdersStatus = async (storeID, orderID, status) => {
  try {
    const response = await axios.put(`${baseURL}/changeOrdersStatus`, {
      storeID,
      orderID,
      status,
    });
    return response.data;
  } catch (error) {
    console.error('Error changing order status:', error);
    return { message: error.message, exception: true };
  }
};

const getOrdersByStoreID = async (storeID) => {
  try {
    const response = await axios.get(`${baseURL}/ordersByStoreID/${storeID}`);
    return response.data;
  } catch (error) {
    console.error('Error getting orders by store ID:', error);
    return { message: error.message, exception: true };
  }
};

const OrderService = {
  addOrder,
  changeOrdersStatus,
  getOrdersByStoreID,
};

export default OrderService;
