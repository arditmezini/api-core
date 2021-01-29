import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtService } from '../services';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private jwtService: JwtService) {}

  canActivate() {
    const token = this.jwtService.getToken();

    if (token) {
      this.jwtService.validateToken(token).subscribe(
        (response) => {
          if (response.result) {
            return true;
          } else {
            this.router.navigate(['auth/login']);
            return false;
          }
        },
        (err) => {
          this.router.navigate(['auth/login']);
          return false;
        }
      );
    } else {
      this.router.navigate(['auth/login']);
      return false;
    }
  }
}
