import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { ToastrService } from 'ngx-toastr';
import { Store } from '@ngrx/store';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  isloginFailed!: boolean;
  constructor(private fb: FormBuilder, private router: Router,
    private loginService: AuthService, private toastr: ToastrService) {

  }

  ngOnInit(): void {
    this.buildLoginForm();
  }

  buildLoginForm(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  login() {
    this.loginService.login(this.loginForm.value).subscribe({
      next: (res) => {
        this.toastr.success('Login successful!', '', { timeOut: 3000 });
        this.router.navigate(['/dashboard']);
      },
      error: () => {
        this.toastr.error('Invalid Credentials', '', { timeOut: 3000 });
      }
    });
  }

  register() { 
    this.router.navigate(['/register']);
  }


}
