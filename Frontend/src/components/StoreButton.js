import React from 'react';

const StoreButton = ({ store, onClick }) => {
  return (
    <button className="store-button" onClick={() => onClick(store.id)}>
      <img src={store.imageUrl} alt={store.name} />
      <span>{store.name}</span>
    </button>
  );
};

export default StoreButton;