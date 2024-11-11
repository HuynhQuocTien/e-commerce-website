import axios from 'axios';
import * as urlApi from '../api/cogfig.api';

export default function axiosInstance (endpoint, method = 'GET', body){
    
    return axios({
        method: method,
        url: `${urlApi.API_URL}/${endpoint}`,
        data: body,
    })
    
}