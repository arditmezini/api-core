import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JwtService, User, UserService, Response, Login } from 'src/app/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  formLogin: FormGroup;

  constructor(
    public userService: UserService,
    private jwtService: JwtService,
    private fb: FormBuilder
  ) {
    this.formLogin = this.fb.group({
      Email: ['', Validators.email],
      Password: ['', [Validators.required, Validators.minLength(4)]],
    });
  }

  ngOnInit(): void {}

  onSubmit() {
    var login = <Login>{
      email: this.formLogin.value.Email,
      password: this.formLogin.value.Password,
    };

    this.userService.login(login).subscribe(
      (res: Response<User>) => {
        var user = res.result;
        this.jwtService.saveToken(user.token);
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
