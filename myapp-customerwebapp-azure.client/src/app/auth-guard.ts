import { Injectable } from '@angular/core';
import { Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './login/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard {

  constructor(private router: Router,private authService:AuthService) {}

  isAuthenticated(): boolean {
    // Replace with your actual auth check logic
    return !!sessionStorage.getItem('token');
  }

  canActivate(): boolean | UrlTree {
    if (this.authService.isAuthenticated()) {
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }
}
