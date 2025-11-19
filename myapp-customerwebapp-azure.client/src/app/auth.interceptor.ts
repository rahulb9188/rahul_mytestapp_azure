import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { catchError, switchMap, filter, take } from 'rxjs/operators';
import { AuthService } from './login/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  // State to prevent multiple refresh requests from running simultaneously
  private isRefreshing = false;
  // Subject to hold the new access token and notify waiting requests
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // 1. Add Access Token to the initial request
    const token = this.authService.getToken();

    if (token) {
      request = this.addToken(request, token);
    }

    // 2. Handle the request and catch 401 errors
    return next.handle(request).pipe(
      catchError(error => {
        if (error instanceof HttpErrorResponse && error.status === 401) {
          // Check if it's the refresh call itself failing (in which case, logout)
          // We assume the refresh endpoint contains '/refresh'
          if (request.url.includes('/refresh')) {
            this.authService.logout();
            return throwError(() => error);
          }
          // The main logic: Handle an expired Access Token
          return this.handle401Error(request, next);
        }
        return throwError(() => error);
      })
    );
  }

  private addToken(request: HttpRequest<any>, token: string) {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // If we are NOT currently refreshing the token:
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null); // Clear the subject to hold subsequent requests

      const refreshToken = this.authService.getRefreshToken();

      // If we have a refresh token, attempt to refresh
      if (refreshToken) {
        // Assume authService.refreshToken() returns an Observable of the new token object
        return this.authService.refreshToken(refreshToken).pipe(
          switchMap((tokenResponse: any) => {
            this.isRefreshing = false;

            // Save the new tokens (AuthService should handle this internally)
            this.authService.saveTokens(tokenResponse.accessToken, tokenResponse.refreshToken);

            // Push the new Access Token to the Subject for queued requests
            this.refreshTokenSubject.next(tokenResponse.accessToken);

            // Retry the failed request with the new Access Token
            return next.handle(this.addToken(request, tokenResponse.accessToken));
          }),
          catchError((err) => {
            this.isRefreshing = false;
            this.authService.logout(); // Refresh token failed, force logout
            return throwError(() => err);
          })
        );
      } else {
        // No refresh token, force logout immediately
        this.authService.logout();
        return throwError(() => new Error('Refresh token not available.'));
      }
    } else {
      // If we ARE already refreshing the token:
      // Queue the current request by waiting for the refreshTokenSubject to emit the new token
      return this.refreshTokenSubject.pipe(
        filter(token => token !== null), // Wait until a non-null token is available
        take(1), // Take only the first emitted token
        switchMap((token: string) => {
          // Retry the failed request with the new token
          return next.handle(this.addToken(request, token));
        })
      );
    }
  }
}
