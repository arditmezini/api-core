import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtService, User, UserService, Response, Login } from 'src/app/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  formLogin: FormGroup;

  constructor(
    private router: Router,
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

    return this.userService.login(login).subscribe(
      (res: Response<User>) => {
        var user = res.result;
        this.jwtService.saveToken(user.token);
        this.router.navigate(['dashboard']);
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
