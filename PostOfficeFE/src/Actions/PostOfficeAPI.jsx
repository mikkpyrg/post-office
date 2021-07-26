import axios from 'axios';

const baseUrl = process.env.REACT_APP_API_URL;

let axiosInstance = axios.create({
    baseURL: baseUrl,
    validateStatus: (status) => { 
      return status == 200;
    }
});

export default axiosInstance;