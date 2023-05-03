// PageManager.js
import React, { useState, useEffect } from 'react';
import './styles/PageManager.css'
import StoreButton from '../components/StoreButton';
import getPageService from '../services/PageService';
const {getAllStores}=getPageService()
const PageManager = ({ onStoreClick }) => {
  const [stores, setStores] = useState([]);

  useEffect(() => {
    const fetchStores = async () => {
      const storeData = await getAllStores();
      setStores(storeData);
    };
    fetchStores();
  }, []);

  return (
    <div className="page-manager">
      {stores.map((store) => (
        <StoreButton key={store.id} store={store} onClick={onStoreClick} />
      ))}
    </div>
  );
};

export default PageManager;
