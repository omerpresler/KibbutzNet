import React, { useState, useEffect } from 'react';
import './styles/PageManager.css';
import StoreButton from '../components/StoreButton';
import getPageService from '../services/PageService';
import BackButton from '../components/BackButton';

const { getAllStores } = getPageService();

const PageManager = ({ onStoreClick }) => {
  const [stores, setStores] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchStores = async () => {
      const storeData = await getAllStores();
      setStores(storeData); 
      setLoading(false);
    };
    fetchStores();
  }, []);

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div className="page-manager">
      {stores.map((store) => (
        <StoreButton key={store.id} store={store} onClick={onStoreClick} />
      ))}
      <BackButton back></BackButton>
    </div>
  );
};

export default PageManager;
