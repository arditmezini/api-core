import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { AppSettings } from '../common';
import { JwtService } from './jwt.service';
import { Login, Register } from '../models';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private api: ApiService, private jwtService: JwtService) {}

  register(register: Register) {
    return this.api.post(`${AppSettings.ApiV1}/account/register`, register);
  }

  login(login: Login) {
    return this.api.post(`${AppSettings.ApiV1}/account/login`, login);
  }

  isUserAuthenticated() {
    const token = this.jwtService.getToken();
    if (token) {
      this.jwtService.validateToken(token).subscribe(
        (response) => {
          if (response.result) {
            return true;
          } else {
            return false;
          }
        },
        (err) => {
          return false;
        }
      );
    } else {
      return false;
    }
  }
}
