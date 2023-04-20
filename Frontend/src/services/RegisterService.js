import axios from 'axios';
import * as paths from './pathes';
import PurchaseHistoryDisplayer from '../components/PurchaseHistoryDisplayer'
import { Response } from './Response';
export default function GetRegsiterService(){
    const addNewPurhcase=(price,description,accountNumber)=> {
        axios.post(paths.addPurchasePath, {
            price: JSON.stringify(price),description:JSON.stringify(description),accountNumber:JSON.stringify(accountNumber)
        })
    .then(function (response) {
        console.log(response)
        if (response.data===true){
            console.log("added new purhcase in server")
        }
        return false;
    })
    .catch(function (error) {
        console.log(error);
        return false;
    });
      }
      const seePurchaseHistoryUser = (userId) => {
        const fakeData = [
            {
              id: 1,
              Date: "2022-01-01",
              BudgetNumber: "123456",
              EmployeeID: "001",
              Cost: "$10.00",
              Description: "Office Supplies",
            },
            {
              id: 2,
              Date: "2022-02-01",
              BudgetNumber: "234567",
              EmployeeID: "002",
              Cost: "$25.00",
              Description: "Lunch Meeting",
            },
            {
              id: 3,
              Date: "2022-03-01",
              BudgetNumber: "345678",
              EmployeeID: "003",
              Cost: "$100.00",
              Description: "Team Building Activity",
            },
          ];
          const res= Response.create(fakeData, false); 
          console.log(res)
          return res
        axios.post(paths.seePurchaseHistoryUserPath, {
          UserId: userId
        })
        .then(function (response) {
          console.log(response);
        })
        .catch(function (error) {
          console.log(error);
        });
      };
      
      const seePurchaseHistoryStore = (storeId) => {
        const fakeData = [
            {
              id: 1,
              Date: "2022-01-01",
              BudgetNumber: "123456",
              EmployeeID: "001",
              Cost: "$10.00",
              Description: "Office Supplies",
            },
            {
              id: 2,
              Date: "2022-02-01",
              BudgetNumber: "234567",
              EmployeeID: "002",
              Cost: "$25.00",
              Description: "Lunch Meeting",
            },
            {
              id: 3,
              Date: "2022-03-01",
              BudgetNumber: "345678",
              EmployeeID: "003",
              Cost: "$100.00",
              Description: "Team Building Activity",
            },
          ];
          const res= Response.create(fakeData, false); 
          console.log(res)
          return res
        axios.post(paths.seePurchaseHistoryStorePath, {
          StoreId: storeId
        })
        .then(function (response) {
          console.log(response);
        })
        .catch(function (error) {
          console.log(error);
        });
      };

      const seePurchaseHistoryAllStore = (storeId) => {
        console.log(storeId)
        const fakeData = [
            {
              id: 1,
              Date: "2022-01-01",
              BudgetNumber: "123456",
              EmployeeID: "001",
              Cost: "$10.00",
              Description: "Office Supplies",
            },
            {
              id: 2,
              Date: "2022-02-01",
              BudgetNumber: "234567",
              EmployeeID: "002",
              Cost: "$25.00",
              Description: "Lunch Meeting",
            },
            {
              id: 3,
              Date: "2022-03-01",
              BudgetNumber: "345678",
              EmployeeID: "003",
              Cost: "$100.00",
              Description: "Team Building Activity",
            },
          ];
          const res= Response.create(fakeData, false); 
          console.log(res)
          return res
        axios.post(paths.seePurchaseHistoryStorePath, {
          StoreId: storeId
        })
        .then(function (response) {
          console.log(response);
        })
        .catch(function (error) {
          console.log(error);
        });
      };
      
      const seePurchaseHistoryUserAndStore = (storeId, userId) => {
        const fakeData = [
            {
              id: 1,
              Date: "2022-01-01",
              BudgetNumber: "123456",
              EmployeeID: "001",
              Cost: "$10.00",
              Description: "Office Supplies",
            },
            {
              id: 2,
              Date: "2022-02-01",
              BudgetNumber: "234567",
              EmployeeID: "002",
              Cost: "$25.00",
              Description: "Lunch Meeting",
            },
            {
              id: 3,
              Date: "2022-03-01",
              BudgetNumber: "345678",
              EmployeeID: "003",
              Cost: "$100.00",
              Description: "Team Building Activity",
            },
          ];
          const res= Response.create(fakeData, false); 
          console.log(res)
          return res
        axios.post(paths.seePurchaseHistoryUserAndStorePath, {
          StoreId: storeId,
          UserId: userId
        })
        .then(function (response) {
          console.log(response);
        })
        .catch(function (error) {
          console.log(error);
        });
      };
      return {addNewPurhcase,seePurchaseHistoryUser,seePurchaseHistoryAllStore,seePurchaseHistoryUserAndStore}
}