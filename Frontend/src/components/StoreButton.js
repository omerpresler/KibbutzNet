import React from 'react';
import { Navigate } from 'react-router-dom';


const StoreButton = ({ store, onClick }) => {
  return (
    <button className="store-button" onClick={() => onClick(store.id)}>
      <img src={store.imageUrl} alt={store.name} />
      <span>{store.name}</span>
    </button>
  );
};
export default StoreButton;