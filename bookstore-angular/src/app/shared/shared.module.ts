import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoaderComponent } from './loader/loader.component';
import { FooterComponent } from './layout/footer.component';
import { HeaderComponent } from './layout/header.component';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [CommonModule, RouterModule],
  declarations: [LoaderComponent, FooterComponent, HeaderComponent],
  exports: [LoaderComponent, FooterComponent, HeaderComponent, RouterModule],
})
export class SharedModule {}
