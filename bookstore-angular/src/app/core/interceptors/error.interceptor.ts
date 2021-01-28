import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

/*
  Adds a default error handeler to all requests.
*/
@Injectable({
  providedIn: 'root',
})
export class ErrorInterceptor implements HttpInterceptor {
  constructor(public router: Router, public toasterService: ToastrService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    console.log('Intercepted');
    return next.handle(req).pipe(
      retry(1),
      catchError((err: HttpErrorResponse) => {
        let errorMessage = '';
        if (err.error instanceof ErrorEvent) {
          // client-side error
          errorMessage = `Error: ${err.message}`;
        } else {
          // server-side error
          errorMessage = `Error Code: ${err.status}\nMessage: ${err.message}`;

          switch (err.status) {
            case 401: //login
              this.router.navigateByUrl('/login');
              break;

            case 403: //forbidden
              this.router.navigateByUrl('/unauthoried');
              break;

            default:
              console.log('Unknown error');
              break;
          }
        }
        this.toasterService.error(errorMessage);
        return throwError(errorMessage);
      })
    );
  }
}
