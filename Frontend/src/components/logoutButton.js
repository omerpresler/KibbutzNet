// LogoutButton.js
import React from 'react';
import { Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import GetLoginService from '../services/loginService';
const LogoutButton = ({ onClick, ...props }) => {
  const navigate=useNavigate()
  const { user, loginToUser,loginToStore: LoginToStore, logout, isAuthenticated }=GetLoginService()
  
  const buttonStyle = {
    position: 'fixed',
    bottom: '20px',
    right: '100px', // Change the position to avoid overlapping with the back button
    zIndex: 100,
    color: "black"
  };

  function do_logout(){
    logout()
    navigate("/")
  }
  
  return (
    <Button style={buttonStyle} onClick={do_logout} {...props}>
      Logout
    </Button>
  );
};

export default LogoutButton;
