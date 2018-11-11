import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';

import { CreateTransaction } from '../models/createtransaction.interface';
import { Transaction } from '../models/transaction.interface';

import { UserService } from '../../shared/services/user.service';
import { ConfigService } from '../../shared/services/config.service';
import { BaseService } from '../../shared/services/base.service';

import { Observable } from 'rxjs/Rx';

import '../../rxjs-operators';

@Injectable()

export class UserTransactionsService extends BaseService {

    constructor(private http: Http, private configService: ConfigService, private userService: UserService) {
        super();
    }

    createUserTransaction(createTransactionData: CreateTransaction): Observable<Transaction> {
        let authToken = this.userService.getAccessToken();
        let body = JSON.stringify(createTransactionData);
        let headers = new Headers({
            'Content-Type': 'application/json',
            'Authentication': `Bearer ${authToken}`
        });

        this.userService.getAccessToken()
        return this.http
            .post(this.configService.getApiUrl() + "/v1/users/me/transactions", body, { headers })
            .map(res => res.json())
            .catch(this.handleError);
    }

    getCurrentUserTransactions() : Observable<Array<Transaction>> {
        let authToken = this.userService.getAccessToken();
        let headers = new Headers({
            'Content-Type': 'application/json',
            'Authentication': `Bearer ${authToken}`
        });

        return this.http
            .get(this.configService.getApiUrl() + "/v1/users/me/transactions", { headers })
            .map(res => res.json())
            .catch(this.handleError);
    }
}