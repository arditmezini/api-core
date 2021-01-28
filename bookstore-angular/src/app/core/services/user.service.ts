import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ApiService } from '.';
import { AppSettings } from '..';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    constructor(private fb:FormBuilder, private api: ApiService){ }

    formModel = this.fb.group({
        FirstName: ['', Validators.required],
        LastName: ['',Validators.required],
        Email: ['', Validators.email],
        Passwords: this.fb.group({
          Password: ['', [Validators.required, Validators.minLength(4)]],
          ConfirmPassword: ['', Validators.required]
        }, { validator: this.comparePasswords })
    });

    comparePasswords(fb: FormGroup) {
        let confirmPswrdCtrl = fb.get('ConfirmPassword');
        //passwordMismatch
        //confirmPswrdCtrl.errors={passwordMismatch:true}
        if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
          if (fb.get('Password').value != confirmPswrdCtrl.value)
            confirmPswrdCtrl.setErrors({ passwordMismatch: true });
          else
            confirmPswrdCtrl.setErrors(null);
        }
      }
      
      register(){
        var body = {
          FirstName: this.formModel.value.FirstName,
          LastName: this.formModel.value.LastName,
          Email: this.formModel.value.Email,
          Password: this.formModel.value.Passwords.Password,
          Role: 'User'
        };
        return this.api.post(`${AppSettings.ApiV1}/account/register`, body);
      }
}