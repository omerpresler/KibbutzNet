// src/components/OrderManager.js
import React, { useState, useEffect } from "react";
import { addOrder, changeOrdersStatus, ordersByStoreID } from "../services/orderService";

const OrderManager = () => {
  const [storeID, setStoreID] = useState(0);
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    fetchOrders();
  }, [storeID]);

  const fetchOrders = async () => {
    const ordersList = await ordersByStoreID(storeID);
    setOrders(ordersList);
  };

  const handleAddOrder = async (memberID, memberName, description, cost) => {
    await addOrder(storeID, memberID, memberName, description, cost);
    fetchOrders();
  };

  const handleChangeOrderStatus = async (orderID, status) => {
    await changeOrdersStatus(storeID, orderID, status);
    fetchOrders();
  };

  return (
    <div>
      {/* Render a form to add an order and call handleAddOrder */}
      {/* Render a list of orders, allowing the status to be changed by calling handleChangeOrderStatus */}
    </div>
  );
};

export default OrderManager;
