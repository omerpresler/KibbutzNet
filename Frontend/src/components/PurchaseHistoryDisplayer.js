import React from 'react';
import { List, ListItem, ListItemText } from '@mui/material';
import Center from './Center';
export default function PurchaseHistoryDisplayer(ToDisplay) {
  const { dataList } = ToDisplay;
  return (
    <Center>
    <List>
      {dataList.map((data) => (
        <ListItem key={data.PurchaseID}>
          <ListItemText primary={data.Date}/>
          <ListItemText primary={data.BudgetNumber}/>
          <ListItemText primary={data.EmployeeID}/>
          <ListItemText primary={data.Cost}/>
          <ListItemText primary={data.Description}/>
        </ListItem>
      ))}
    </List>
    </Center>
  );
}
