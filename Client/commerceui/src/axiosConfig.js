import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:8000/api/', // replace with your Django API URL
    timeout: 5000 // set a timeout value
});

export default api;
