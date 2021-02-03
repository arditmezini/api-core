import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Register, User, Response } from 'src/app/core';
import { UserService } from 'src/app/core';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
})
export class RegistrationComponent implements OnInit {
  formRegister: FormGroup;

  constructor(
    public userService: UserService,
    private toastr: ToastrService,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.formRegister = 
    this.fb.group({
      FirstName: ['', Validators.required],
      LastName: ['', Validators.required],
      Email: ['', Validators.email],
      Passwords: this.fb.group(
        {
          Password: ['', [Validators.required, Validators.minLength(4)]],
          ConfirmPassword: ['', Validators.required],
        },
        { validator: this.comparePasswords }
      ),
    });
  }

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');

    if (
      confirmPswrdCtrl.errors == null ||
      'passwordMismatch' in confirmPswrdCtrl.errors
    ) {
      if (fb.get('Password').value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else confirmPswrdCtrl.setErrors(null);
    }
  }

  ngOnInit(): void {
    this.formRegister.reset();
  }

  onSubmit() {
    var register = <Register>{
      firstName: this.formRegister.value.FirstName,
      lastName: this.formRegister.value.LastName,
      email: this.formRegister.value.Email,
      password: this.formRegister.value.Passwords.Password,
      role: 'User',
    };

    this.userService.register(register).subscribe(
      (res: Response<User>) => {
        this.toastr.success('New user created!', 'Registration successful');
        this.router.navigate(['auth/login']);
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
