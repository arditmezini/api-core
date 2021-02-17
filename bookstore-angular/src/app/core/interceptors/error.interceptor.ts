import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, throwError } from 'rxjs';
import { retry, catchError, map } from 'rxjs/operators';
import { AppSettings } from '../common/index';

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
    return next.handle(req).pipe(
      //Retry
      retry(AppSettings.RetryApiCall),
      //Response
      map((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          console.log('event--->>>', event);
        }
        return event;
      }),
      //Error
      catchError((err: HttpErrorResponse) => {
        let errorMessage = '';
        if (err.error instanceof ErrorEvent) {
          // client-side error
          errorMessage = `CLIENT - Error: ${err.message}`;
        } else {
          // server-side error
          errorMessage = `SERVER - Error Code: ${err.status}\nMessage: ${err.message}`;

          switch (err.status) {
            case 0: //api not working
              errorMessage = `SERVER - Error: API not reachable, please try again later.`;
              break;

            case 400: //Bad request
              break;

            case 401: //login
              this.router.navigateByUrl('/login');
              break;

            case 403: //forbidden
              this.router.navigateByUrl('/not-found');
              break;

            default:
              errorMessage = `SERVER - Error : An error has occured, please try again.`;
              break;
          }
        }
        this.toasterService.error(errorMessage);
        return throwError(errorMessage);
      })
    );
  }
}
