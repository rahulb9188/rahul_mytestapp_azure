import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { Login } from '../shared/models/login.model';
import { ToastrService } from 'ngx-toastr';
import { Store } from '@ngrx/store';
import { loginSuccess } from '../shared/auth.store';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  isloginFailed!: boolean;
  constructor(private fb: FormBuilder, private router: Router,
    private loginService: AuthService, private toastr: ToastrService,private store:Store) {

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
        const loggedInUser = {
          username: res?.loginUser?.userName,
          email: res?.loginUser?.email,
          name: res?.loginUser?.name
          //accessToken: res.accessToken,
          //refreshToken: res.refreshToken
        };
        this.store.dispatch(loginSuccess({ user: loggedInUser }));
        console.log('Login successful', res);
         this.toastr.success('Hello world!','', {
          timeOut: 3000,
        });
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        console.error('Login failed', err);
        this.toastr.error('Invalid Credentials', '', {
          timeOut: 3000,
        });
      }
      // console.log(res);
      // next: () => this.router.navigate(['/dashboard']), // or wherever you go after login
      // error: () => this.errorMessage = 'Invalid credentials'
      // this.router.navigate(['dashboard']);
      // if (this.loginForm.get('email')?.value == 'rbachche@gmail.com' && this.loginForm.get('password')?.value == '123') {
      //   sessionStorage.setItem('token', 'true')
      //   this.router.navigate(['dashboard']);
      // } else {
      //   this.isloginFailed = true;
      // }
    })


  }

  register() { 
    this.router.navigate(['/register']);
  }


}
