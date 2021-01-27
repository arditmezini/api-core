import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { BaseUrlInterceptor } from "./interceptors/BaseUrlInterceptor";
import { ErrorInterceptor } from "./interceptors/ErrorInterceptor";
import { LoaderInterceptor } from "./interceptors/LoaderInterceptor";

import { UserService } from "./services/user.service";
import { LoaderService } from "./services/loader.service";

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
          UserService,
          LoaderService
    ],
    declarations: []
})
export class CoreModule {}