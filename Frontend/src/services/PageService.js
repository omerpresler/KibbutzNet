// storeService.js
import axios from 'axios';
import * as paths from './pathes';
import { Response } from './Response';
const getAllStoresPath = paths.back_path + paths.store_controller_path + paths.get_all_stores;

export default function getPageService() {
   async function getAllStores() {
    try {
      const response = await axios.get(getAllStoresPath);
      const stores = response.data.map(store => {
        return {
          id: store.id,
          name: store.name,
          imageUrl: store.imageUrl,
          
        };
      });
      return stores;
    } catch (error) {
      console.error(error);
      return [];
    }
  }

  return { getAllStores };
}
