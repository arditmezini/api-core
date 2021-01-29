import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { UserService } from '../services';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private userService: UserService) {}

  canActivate() {
    if (!this.userService.isUserAuthenticated()) {
      this.router.navigate(['auth/login']);
      return false;
    }
    return true;
  }
}
