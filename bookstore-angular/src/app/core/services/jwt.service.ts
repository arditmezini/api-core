import { Injectable } from '@angular/core';
import { AppSettings } from '../common';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root',
})
export class JwtService {
  constructor(private api: ApiService) {}

  getToken(): string {
    return window.localStorage[AppSettings.JwtToken];
  }

  saveToken(token: string) {
    window.localStorage[AppSettings.JwtToken] = token;
  }

  destroyToken() {
    window.localStorage.removeItem(AppSettings.JwtToken);
  }

  validateToken(tokenValue: string) {
    var body = {
      token: tokenValue
    };
    return this.api.post(`${AppSettings.ApiV1}/account/validate`, body);
  }
}