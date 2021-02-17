import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core';

@Component({
  selector: 'app-layout-header',
  templateUrl: './header.component.html',
})
export class HeaderComponent {
  constructor(private userService: UserService) {}

  logout(){
    return this.userService.logout();
  }
}