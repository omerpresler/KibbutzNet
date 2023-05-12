import React from 'react';
import { Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const BackButton = ({ text = 'Back', ...props }) => {
  const navigate = useNavigate();

  const goBack = () => {
    navigate(-1);
  };

  const buttonStyle = {
    position: 'fixed',
    bottom: '20px',
    right: '20px',
    zIndex: 100,
    color: "black"
  };

  return (
    <Button style={buttonStyle} onClick={goBack} {...props}>
      {text}
    </Button>
  );
};

export default BackButton;
