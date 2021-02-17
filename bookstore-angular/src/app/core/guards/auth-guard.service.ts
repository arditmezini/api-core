import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtService, UserService } from '../services';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private jwtService: JwtService, private userService: UserService) {}

  canActivate() {
    if (this.jwtService.isTokenExpired()){
      this.router.navigate(['auth/login']);
      return false;
    }
    return true;
  }
}
