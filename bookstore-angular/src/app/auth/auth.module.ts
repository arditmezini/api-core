import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, AuthRoutingModule],
  exports:[LoginComponent, RegistrationComponent],
  declarations: [LoginComponent, RegistrationComponent],
})
export class AuthModule {}
