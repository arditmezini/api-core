import { Injectable } from '@angular/core';
import { AppSettings } from '../common';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root',
})
export class JwtService {
  constructor(private api: ApiService) {}

  getToken(): String {
    return window.localStorage[AppSettings.JwtToken];
  }

  saveToken(token: String) {
    window.localStorage[AppSettings.JwtToken] = token;
  }

  destroyToken() {
    window.localStorage.removeItem(AppSettings.JwtToken);
  }

  validateToken(tokenValue: String) {
    var body = {
      token: tokenValue
    };
    return this.api.post(`${AppSettings.ApiV1}/account/validate`, body);
  }
}