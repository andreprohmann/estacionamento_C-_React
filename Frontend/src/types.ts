
export interface OcupacaoAtual {
  totalSpots: number;
  occupied: number;
  available: number;
}

export interface VeiculoDto {
  placa: string;
  modelo?: string;
  cor?: string;
  nomeMotorista?: string;
  timeIn?: string;
  timeOut?: string;
}
