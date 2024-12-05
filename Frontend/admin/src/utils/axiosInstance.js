import axios from 'axios';
import * as rootApi from '../apis/rootApi';
import { Redirect } from 'react-router-dom';
import { message } from 'antd';

export default function axiosInstance (endpoint, method = 'GET', body){
    const token = localStorage.getItem("access_token") ? JSON.parse(localStorage.getItem("access_token")): "";
    
    return axios({
        method: method,
        url: `${rootApi.API_URL}/${endpoint}`,
        data: body,
        headers: {
            Authorization: `Bearer ${token.value}` 
        }
    }).catch(err => {
        console.error(`Error in API ${endpoint}:`, err);
        message.error("Something went wrong with the server!");
        throw err;
    });
    
}