import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { JwtService, UserService } from '../services';
import decode from 'jwt-decode';
import { JwtPayload } from '../models';

@Injectable({
  providedIn: 'root',
})
export class RoleAuthGuardService implements CanActivate {
  constructor(
    private userService: UserService,
    private jwtService: JwtService,
    private router: Router
  ) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRole = route.data.Role;
    const token = this.jwtService.getToken();
    const tokenPayload = decode<JwtPayload>(token);

    if (
      !this.userService.isUserAuthenticated() ||
      tokenPayload.role !== expectedRole
    ) {
      this.router.navigate(['auth/login']);
      return false;
    }
    return true;
  }
}
