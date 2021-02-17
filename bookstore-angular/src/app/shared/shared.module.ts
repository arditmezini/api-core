import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoaderComponent } from './loader/loader.component';
import { FooterComponent } from './layout/footer.component';
import { HeaderComponent } from './layout/header.component';
import { RouterModule } from '@angular/router';
import { AuthLayoutComponent } from './layout/auth/auth-layout.component';
import { HomeLayoutComponent } from './layout/home/home-layout.component';

@NgModule({
  imports: [CommonModule, RouterModule],
  declarations: [LoaderComponent, FooterComponent, HeaderComponent, AuthLayoutComponent, HomeLayoutComponent],
  exports: [LoaderComponent, FooterComponent, HeaderComponent, AuthLayoutComponent, HomeLayoutComponent, RouterModule],
})
export class SharedModule {}
