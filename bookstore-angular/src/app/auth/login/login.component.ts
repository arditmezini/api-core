import { Component, OnInit } from '@angular/core';
import { JwtService, UserService } from 'src/app/core';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(public userService: UserService, private jwtService: JwtService) { }

  ngOnInit(): void { }

  onSubmit(){
    this.userService.login().subscribe(
      (res:any) => {
        
      },
      err => {
        console.log(err);
      }
    )
  };

}
