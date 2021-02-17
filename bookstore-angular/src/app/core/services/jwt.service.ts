import { Injectable } from '@angular/core';
import { AppSettings } from '../common';
import { ApiService } from './api.service';
import { JwtPayload } from '../models';
import decode from 'jwt-decode';

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

  getTokenExpirationDate(token: string): Date {
    const decoded = <JwtPayload>decode(token);

    if (decoded.exp === undefined) return null;

    const date = new Date(0); 
    date.setUTCSeconds(decoded.exp);
    return date;
  }

  isTokenExpired(token?: string): boolean {
    if(!token) token = this.getToken();
    if(!token) return true;

    const date = this.getTokenExpirationDate(token);
    if(date === undefined) return false;
    return !(date.valueOf() > new Date().valueOf());
  }
}