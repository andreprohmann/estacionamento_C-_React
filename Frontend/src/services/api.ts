import axios from 'axios';

export const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  timeout: 15000,
});

export interface OcupacaoAtual {
  totalSpots: number;
  occupied: number;
  available: number;
}

export async function getOcupacaoAtual(): Promise<OcupacaoAtual> {
  const { data } = await api.get<OcupacaoAtual>('/vagas/ocupacao-atual');
  return data;
}

export async function listVehicles() {
  const { data } = await api.get<{ id: number; placa: string; modelo: string; cor: string }[]>('/veiculos');
  return data;
}
