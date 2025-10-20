
import { api } from './api';
import type { OcupacaoAtual, VeiculoDto } from '../types';

const routes = {
  ocupacaoAtual: '/vagas/ocupacao-atual',
  veiculos: '/veiculos',
  checkin: '/estacionamento/checkin',
  checkout: '/estacionamento/checkout',
} as const;

export async function getOcupacaoAtual(): Promise<OcupacaoAtual> {
  const { data } = await api.get<OcupacaoAtual>(routes.ocupacaoAtual);
  return data;
}

export async function listVehicles(): Promise<VeiculoDto[]> {
  const { data } = await api.get<VeiculoDto[]>(routes.veiculos);
  return data;
}

export async function checkinVehicle(payload: VeiculoDto) {
  const { data } = await api.post(routes.checkin, payload, {
    headers: { 'Content-Type': 'application/json' },
  });
  return data;
}

export async function checkoutVehicle(placa: string) {
  const { data } = await api.post(routes.checkout, { placa }, {
    headers: { 'Content-Type': 'application/json' },
  });
  return data; // esperado: { price: number, vehicle: VeiculoDto }
}
