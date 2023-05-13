// storeService.js
import axios from 'axios';
import * as paths from './pathes';
import { Response } from './Response';


export default function getPageService() {
  const fakeData = [
    {
      id: 1,
      name: 'the hair sallon',
      imageUrl: 'https://cdn.pixabay.com/photo/2017/06/24/02/56/art-2436545_960_720.jpg',
    },
    {
      id: 2,
      name: 'the dining room',
      imageUrl: 'https://cdn.pixabay.com/photo/2015/12/09/17/11/vegetables-1085063_960_720.jpg',
    },
    {
      id: 3,
      name: 'the gardner',
      imageUrl: 'https://cdn.pixabay.com/photo/2023/04/29/09/43/bee-7958148_960_720.jpg',
    },
  ];


  function getStore(storeId) {
    return axios.post(paths.getAllStores,{storeId})
      .then(res=> {
        const response = Response.create(res.data.value, res.data.exceptionHasOccured,res.data.errorMessage);;
        return response
      })
      .catch(res=> {
        const response = Response.create(res.data.value, res.data.exceptionHasOccured,res.data.errorMessage);;
        alert(res.data.exceptionHasOccured)
        return [];
      })
  };

  function getAllStores() {
    return axios.post(paths.getStore)
      .then(res=> {
        const response = Response.create(res.data.value, res.data.exceptionHasOccured,res.data.errorMessage);;
        const stores = response.data.map(store => {
          return {
            id: store.id,
            name: store.name,
            imageUrl: store.imageUrl,
            
          };
        });
        return stores;
      })
      .catch(res=> {
        const response = Response.create(res.data.value, res.data.exceptionHasOccured,res.data.errorMessage);;
        alert(res.data.exceptionHasOccured)
        return [];
      })
  };
  return { getAllStores };
}
