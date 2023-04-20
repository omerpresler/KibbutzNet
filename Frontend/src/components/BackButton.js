import React from 'react';
import { Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const BackButton = ({ text = 'Back', ...props }) => {
  const navigate = useNavigate();

  const goBack = () => {
    navigate(-1);
  };

  return (
    <Button onClick={goBack} {...props}>
      {text}
    </Button>
  );
};

export default BackButton;
