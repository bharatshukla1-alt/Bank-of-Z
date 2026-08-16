import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CustomerProfile, AccountDetails, FundTransferRequest, DepositWithdrawalRequest, TransactionResult } from '../models/crm.models';

@Injectable({
  providedIn: 'root'
})
export class CrmService {
  private readonly apiUrl = '/api/v1/crm';

  constructor(private http: HttpClient) {}

  getCustomer(company: string, customerNo: string): Observable<CustomerProfile> {
    return this.http.get<CustomerProfile>(`${this.apiUrl}/customers/${company}/${customerNo}`);
  }

  saveCustomer(customer: CustomerProfile): Observable<CustomerProfile> {
    return this.http.post<CustomerProfile>(`${this.apiUrl}/customers`, customer);
  }

  getAccount(company: string, accountNo: string): Observable<AccountDetails> {
    return this.http.get<AccountDetails>(`${this.apiUrl}/accounts/${company}/${accountNo}`);
  }

  saveAccount(account: AccountDetails): Observable<AccountDetails> {
    return this.http.post<AccountDetails>(`${this.apiUrl}/accounts`, account);
  }

  transferFunds(request: FundTransferRequest): Observable<TransactionResult> {
    return this.http.post<TransactionResult>(`${this.apiUrl}/transactions/transfer`, request);
  }

  processDepositWithdrawal(request: DepositWithdrawalRequest): Observable<TransactionResult> {
    return this.http.post<TransactionResult>(`${this.apiUrl}/transactions/cash`, request);
  }
}
