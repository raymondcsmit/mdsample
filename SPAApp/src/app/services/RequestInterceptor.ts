import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpResponse, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from "rxjs/operators";

@Injectable()
export class RequestInterceptor implements HttpInterceptor {
  public headers: HttpHeaders;
  private tokenkey = "token";
  constructor( private router: Router) { }
  private getLocalToken(): any {
    var ltoken = JSON.parse(localStorage.getItem(this.tokenkey));
    return ltoken;
    //return localStorage.getItem(this.tokeyKey) ? localStorage.getItem(this.tokeyKey):'';
  }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
      request = request.clone({
        setHeaders: {
        //  'Authorization': 'Bearer ' + this.getLocalToken(),
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        }
      });

    return next.handle(request).pipe(
      tap((event: HttpEvent<any>) => {
        console.log(event)
        if (event instanceof HttpResponse && event.status === 201) {
          console.log(event);//this.toastr.success("Object created.");
        }
      },
        (err: any) => {
        if (err instanceof HttpErrorResponse) {
          console.log(this.handleAuthError(err));
        }
      })
    );
    
  }
  private handleAuthError(err: HttpErrorResponse): any {
    //handle your auth error or rethrow
    let message: string;
    message = "error status:" + err.status;
    message += " ";
    message += "error message:" + err.message;
    
    return message;
  }
}
