import { api } from './api';
import { ApiResponse, ParkingStatus, Vehicle } from '../types';

const routes = {
  status: '/api/estacionamento/status',
  vehicles: '/api/veiculos',
  checkin: '/api/estacionamento/checkin',
  checkout: '/api/estacionamento/checkout',
};

export async function getStatus() {
  const { data } = await api.get<ApiResponse<ParkingStatus>>(routes.status);
  return data.data;
}

export async function listVehicles() {
  const { data } = await api.get<ApiResponse<Vehicle[]>>(routes.vehicles);
  return data.data;
}

export async function checkinVehicle(payload: Pick<Vehicle, 'plate' | 'model' | 'color' | 'driverName'>) {
  const { data } = await api.post<ApiResponse<Vehicle>>(routes.checkin, payload);
  return data.data;
}

export async function checkoutVehicle(plate: string) {
  const { data } = await api.post<ApiResponse<{ price: number; vehicle: Vehicle }>>(routes.checkout, { plate });
  return data.data;
}