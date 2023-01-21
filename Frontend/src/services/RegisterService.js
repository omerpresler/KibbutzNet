import axios from 'axios';
import * as paths from './pathes';
const addNewPurchasePath=paths.back_path+paths.login_controller_path+"/addNewPruchase" 
const seePurchaseHistoryPath=paths.back_path+paths.login_controller_path+"/seePurchaseHistory" 

export default function GetRegsiterService(){
    const addNewPurhcase=(price,description,accountNumber)=> {
        axios.post(addNewPurchasePath, {
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
    const seePurchaseHistory=(from,to)=> {
        axios.post(seePurchaseHistory, {
            from: JSON.stringify(from),
            to:JSON.stringify(to)
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
      return {addNewPurhcase,seePurchaseHistory}
}