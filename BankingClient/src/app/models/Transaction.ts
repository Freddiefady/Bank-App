export interface Transaction {
  transactionId: number;
  clientId: number;
  clientName: string;
  accountNumber: string;
  transactionType: string;
  transactionAmount: number;
  balanceAfterTransaction: number;
  transactionDate: Date;
  description?: string;
}

export interface CreateTransactionRequest {
  clientId: number;
  transactionType: string;
  transactionAmount: number;
  description?: string;
}
