export type Plate = string;

export interface Vehicle {
  id?: string;
  plate: Plate;
  model?: string;
  color?: string;
  driverName?: string;
  timeIn?: string;   // ISO
  timeOut?: string;  // ISO
}

export interface ParkingStatus {
  totalSpots: number;
  occupied: number;
  available: number;
}


export interface OcupacaoAtual {
  totalSpots: number;
  occupied: number;
  available: number;
}


export interface ApiResponse<T> {
  data: T;
  message?: string;
}
