import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(public userService: UserService) { }

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
