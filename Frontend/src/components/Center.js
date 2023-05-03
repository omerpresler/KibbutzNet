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
        minHeight: '100vh',
        padding: '10rem',
        width: '100%',
      }}
    >
      <Grid item xs={12} sm={10} md={8} lg={6} xl={4} rowSpacing={3}>
        {props.children}
      </Grid>
    </Grid>
  );
}
