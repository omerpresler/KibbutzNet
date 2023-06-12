import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { CssBaseline } from '@mui/material/';

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
    background: {
      default: '#1034A6', // Replace with the desired background color
    },
  },
  typography:{
    fontFamily:'"IBM Plex Sans"',
    fontSize: 30,
  },
  components: {
    MuiTextField: {
      styleOverrides: {
        root: {
          padding: '10px',
          border: '1px solid #444',
          borderRadius: '4px',
          fontSize: '1rem',
          color: '#ccc',
          background: '#333',
          '&:hover': {
            border: '1px solid #666',
          },
          '& .MuiOutlinedInput-root': {
            '& fieldset': {
              borderColor: '#666',
            },
            '&:hover fieldset': {
              borderColor: '#888',
            },
            '&.Mui-focused fieldset': {
              borderColor: '#aaa',
            },
          },
        },
      },
    },
    MuiButton: {
      styleOverrides: {
        root: {
          fontSize: '3.5rem', // Adjust as necessary
        },
      },
    },
    MuiTypography: {
      defaultProps: {
        align: 'right', // Set global alignment
      },
    },

  },
});


ReactDOM.render(
  <React.StrictMode>
      <ThemeProvider theme={darkTheme}>
        <CssBaseline />
        <App />
      </ThemeProvider>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();