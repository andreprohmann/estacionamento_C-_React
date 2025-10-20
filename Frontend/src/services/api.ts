
import axios from 'axios';
import { API_BASE_URL } from '../env';

export const api = axios.create({
  baseURL: API_BASE_URL, // vazio em dev => caminhos relativos => proxy do Vite
  timeout: 15000,
});

api.interceptors.response.use(
  (res) => res,
  (err) => Promise.reject(err)
);
