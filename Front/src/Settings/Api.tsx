
import axios from 'axios';
import AppSettings from '../settings/AppSettings';


const _Api = axios.create({ baseURL: AppSettings.CovidApiUrl });
_Api.interceptors.request.use(async config => { return config; });


const Api = axios.create({
  baseURL: AppSettings.CovidApiUrl

});

Api.interceptors.request.use(async config => {


  return config;
});

export default Api;