import { Injectable } from '@angular/core';
import { Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './login/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard {

  constructor(private router: Router,private authService:AuthService) {}

  isAuthenticated(): boolean {
    return !!sessionStorage.getItem('access_token');
  }

  canActivate(): boolean | UrlTree {
    if (this.authService.isAuthenticated()) {
      return true;
    }
    return this.router.parseUrl('/login');
  }
}
