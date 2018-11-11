import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { HttpParams } from "@angular/common/http";

import { UserRegistration } from '../models/user.registration.interface';
import { ConfigService } from './config.service';

import { BaseService } from "./base.service";

import { Observable } from 'rxjs/Rx';
import { BehaviorSubject } from 'rxjs/Rx';

import '../../rxjs-operators';

@Injectable()

export class UserService extends BaseService {

  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  authNavStatus$ = this._authNavStatusSource.asObservable();

  private loggedIn = false;

  constructor(private http: Http, private configService: ConfigService) {
    super();
    this.loggedIn = !!localStorage.getItem('access_token');

    this._authNavStatusSource.next(this.loggedIn);
  }

  getAccessToken() {
    return localStorage.getItem('access_token');
  }

  register(registrationData: UserRegistration): Observable<UserRegistration> {
    let body = JSON.stringify(registrationData);
    let headers = new Headers({ 'Content-Type': 'application/json' });
    return this.http.post(this.configService.getApiUrl() + "/v1/users", body, { headers })
      .map(res => true)
      .catch(this.handleError);
  }

  login(username: string, password: string) {
    const headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });

    var authCredentials = this.configService.getAuthClientCredentials();
    let authBody = new HttpParams()
      .set('username', username)
      .set('password', password)
      .set('grant_type', 'password')
      .set('client_id', authCredentials.ClientId)
      .set('client_secret', authCredentials.ClientSecret);

    return this.http
      .post(this.configService.getAuthUrl(), authBody.toString(), { headers })
      .map(res => res.json())
      .map(res => {
        localStorage.setItem('access_token', res.access_token);
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
        return true;
      })
      .catch(this.handleError);
  }

  logout() {
    localStorage.removeItem('access_token');
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
  }

  isLoggedIn() {
    return this.loggedIn;
  }
}