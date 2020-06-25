import Api from '../settings/Api';
import { CovidModel } from 'Models/CovidModel'; 




export default class CovidApiService {
  static getListAllCollegection = async () => {
    return await Api.get('Covid/GetAllCollection').then((result) => {
       let collections: CovidModel[] = result.data
      return collections
    });
  }
  static getLastUpdate = async () => {

    return await Api.get('Covid/GetLastUpdate').then((result) => {
      return  result.data as CovidModel
    });

  };
}