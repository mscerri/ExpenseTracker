import { Injectable } from '@angular/core';

@Injectable()
export class ConfigService {

    _apiUrl: string;

    _authUrl: string;
    _authClientId: string;
    _authClientSecret: string;

    constructor() {
        this._apiUrl = 'http://localhost:56275/api';
        this._authUrl = 'http://localhost:56275/connect/token';
        this._authClientId = '96a32b4f-53d6-4001-91ca-97fbdf218b23';
        this._authClientSecret = 'a77af739-2f7a-45de-980b-c73c2a404e0c';
    }

    getApiUrl() {
        return this._apiUrl;
    }

    getAuthUrl() {
        return this._authUrl;
    }

    getAuthClientCredentials() {
        return {
            ClientId: this._authClientId,
            ClientSecret: this._authClientSecret
        }
    }
}
