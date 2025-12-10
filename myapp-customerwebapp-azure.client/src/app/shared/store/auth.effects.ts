import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Router } from '@angular/router';
import { AuthService } from '../../login/auth.service';
import * as AuthActions from './auth.actions';
import { mergeMap, of, tap, catchError, map } from 'rxjs';

@Injectable()
export class AuthEffects {

  constructor(
    private actions$: Actions,
    private authService: AuthService,
    private router: Router
  ) { }

  checkAuth$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.checkAuth),
      mergeMap(() => {
        const token = localStorage.getItem('access_token');

        if (token) {
          return of(AuthActions.loadUserProfile());
        }

        return of(AuthActions.logout());
      })
    )
  );

  loadUserProfile$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.loadUserProfile),
      mergeMap(() =>
        this.authService.getProfile().pipe(
          map(user => AuthActions.loadUserProfileSuccess({ user })),
          catchError(() => of(AuthActions.loadUserProfileFailure()))
        )
      )
    )
  );

  logoutRedirect$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(AuthActions.logout),
        tap(() => {
          console.log('Logout => Redirecting to /login');
          setTimeout(() => this.router.navigate(['/login']), 0);
        })
      ),
    { dispatch: false }
  );
}
