import axios from 'axios';
import * as paths from './pathes';
import PurchaseHistoryDisplayer from '../components/PurchaseHistoryDisplayer'
const addNewPurchasePath=paths.back_path+paths.register_page_path+"/addPurchase" 
const seePurchaseHistoryPath=paths.back_path+paths.register_page_path+"/seePurchaseHistory" 

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
    // const seePurchaseHistory=(from,to)=> {
    //     console.log("trying to see purchase history")
    //     axios.post(seePurchaseHistoryPath, {
    //         from: JSON.stringify(from),
    //         to:JSON.stringify(to)
    //     })
    // .then(function (response) {
       
        
    //     if (response.data!=null){
    //         alert(response)
    //         return PurchaseHistoryDisplayer(response)
    //     }
    // })
    // .catch(function (error) {
    //     console.log("see purchase history error")
    //     console.log(error);
    //     return <h1> there was an error </h1>;
    // });
    //   }
      return {addNewPurhcase}
}