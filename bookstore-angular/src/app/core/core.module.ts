import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { ApiService, JwtService, LoaderService, UserService } from "./services";
import { BaseUrlInterceptor, ErrorInterceptor, LoaderInterceptor, TokenInterceptor } from "./interceptors";


@NgModule({
    imports: [
        CommonModule
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS, useClass: BaseUrlInterceptor, multi:true
          },
          {
            provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi:true
          },
          {
            provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true
          },
          {
            provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi:true
          },
          ApiService,
          UserService,
          LoaderService,
          JwtService
    ],
    declarations: []
})
export class CoreModule {}