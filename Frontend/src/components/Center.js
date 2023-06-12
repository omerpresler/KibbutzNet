import React from 'react';
import { Grid } from '@mui/material';

export default function Center(props) {
  return (
    <Grid
      container
      direction="column"
      alignItems="center"
      justifyContent="center"
      sx={{
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        padding: '100px',
        backgroundColor: '#00000',
        minHeight: '100vh',
        padding: '5rem',
        width: '100%',
        backgroundColor: '#add8e6',  // Light blue background
      }}
    >
      <Grid 
        item 
        xs={12} 
        sm={10} 
        md={8} 
        lg={6} 
        xl={4} 
        rowSpacing={3} 
        sx={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          padding: '10px',
          backgroundColor: '#00000',
          boxShadow: '0px 0px 5px 0px rgba(0,0,0,0.1)',  // Box shadow for items
          borderRadius: '10px',  // Rounded corners
          backgroundColor: '#fff',  // White background for items
          padding: '1rem',  // Padding inside items
        }}
      >
        {props.children}
      </Grid>
    </Grid>
  );
}
