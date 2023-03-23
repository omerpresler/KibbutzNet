import { Button } from '@mui/material';
import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router';
import Center from './Center';

export default function DataDisplay(fetchDataFunc,DataDisplayer,start) {
  
    const [data, setData] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    
    useEffect(() => {
      if (start){
      async function fetchData() {
        try {
          const response = await fetchDataFunc();
          setData(response.data);
          setIsLoading(false);
        } catch (error) {
          console.error(error);
        }
      }
      fetchData();}
    }, [fetchDataFunc])
  
    return (
      
      <div>
        {isLoading ? (
          <div>Loading...</div>
        ) : (
          DataDisplayer(data)
        )}
      </div>
    );
  }
