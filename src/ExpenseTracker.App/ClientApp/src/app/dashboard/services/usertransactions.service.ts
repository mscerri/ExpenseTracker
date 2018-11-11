import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';

import { CreateTransaction } from '../models/createtransaction.interface';
import { Transaction } from '../models/transaction.interface';

import { UserService } from '../../shared/services/user.service';
import { ConfigService } from '../../shared/services/config.service';
import { BaseService } from '../../shared/services/base.service';

import { Observable } from 'rxjs/Rx';

import '../../rxjs-operators';
import { TransactionCategory } from '../models/transactioncategory.interface';
import { Currency } from '../models/currency.interface';

@Injectable()

export class UserTransactionsService extends BaseService {

    constructor(private http: Http, private configService: ConfigService, private userService: UserService) {
        super();
    }

    createUserTransaction(createTransactionData: CreateTransaction): Observable<Transaction> {        
        let body = JSON.stringify(createTransactionData);
        let headers = this.getHeaders();
        return this.http
            .post(this.configService.getApiUrl() + "/v1/users/me/transactions", body, { headers })
            .map(res => res.json())
            .catch(this.handleError);
    }

    deleteUserTransaction(transaction: Transaction) : Observable<boolean> {
        let headers = this.getHeaders();
        return this.http
            .delete(this.configService.getApiUrl() + `/v1/transactions/${transaction.id}`, { headers })
            .map(res => true)
            .catch(this.handleError);
    }

    getCurrentUserTransactions() : Observable<Transaction[]> {
        let headers = this.getHeaders();
        return this.http
            .get(this.configService.getApiUrl() + "/v1/users/me/transactions", { headers })
            .map(res => res.json())
            .catch(this.handleError);
    }

    getCurrencies() : Observable<Currency[]> {
        let headers = this.getHeaders();
        return this.http
            .get(this.configService.getApiUrl() + "/v1/currencies", { headers })
            .map(res => res.json())
            .catch(this.handleError);
    }

    getCategories() : Observable<TransactionCategory[]> {
        let headers = this.getHeaders();
        return this.http
            .get(this.configService.getApiUrl() + "/v1/categories", { headers })
            .map(res => res.json())
            .catch(this.handleError);
    }

    private getHeaders() : Headers {
        let authToken = this.userService.getAccessToken();
        return new Headers({
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${authToken}`
        });
    }
}
