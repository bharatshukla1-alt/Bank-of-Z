export interface CustomerProfile {
  company: string;
  customerNumber: string;
  title: string;
  firstName: string;
  middleInitial?: string;
  lastName: string;
  addressLine1: string;
  addressLine2?: string;
  city: string;
  postcode: string;
  country: string;
  dateOfBirth: string;
  sortCode: string;
  creditScore: number;
  scoreDate: string;
  statusMessage?: string;
}

export interface AccountDetails {
  company: string;
  customerNumber: string;
  accountNumber: string;
  accountType: string;
  interestRate: number;
  overdraftLimit: number;
  sortCode: string;
  openDate: string;
  lastStatementDate: string;
  nextStatementDate: string;
  availableBalance: number;
  actualBalance: number;
  statusMessage?: string;
}

export interface FundTransferRequest {
  company: string;
  fromAccountNumber: string;
  toAccountNumber: string;
  fromSortCode: string;
  toSortCode: string;
  amount: number;
  fastCodes?: string[];
}

export interface DepositWithdrawalRequest {
  company: string;
  accountNumber: string;
  sortCode: string;
  sign: '+' | '-';
  amount: number;
}

export interface TransactionResult {
  success: boolean;
  fromActualBalance?: number;
  fromAvailableBalance?: number;
  toActualBalance?: number;
  toAvailableBalance?: number;
  message: string;
}
