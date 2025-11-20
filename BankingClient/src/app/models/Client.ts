export interface Client {
  clientId: number;
  name: string;
  nationalId: string;
  age: number;
  accountNumber: string;
  maxCreditBalance: number;
  currentBalance: number;
  createdDate: Date;
  isActive: boolean;
}

export interface CreateClientRequest {
  name: string;
  nationalId: string;
  age: number;
  accountNumber: string;
  maxCreditBalance: number;
}
