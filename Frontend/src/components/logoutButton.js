import React from 'react';
import { Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const LogoutButton = ({ text = 'Logout', ...props }) => {
  const navigate = useNavigate();

  const handleLogout = () => {
    // Clear all data from local storage
    localStorage.clear();

    // Navigate back to the index page
    navigate('/');
  };

  return (
    <Button onClick={handleLogout} {...props}>
      {text}
    </Button>
  );
};

export default LogoutButton