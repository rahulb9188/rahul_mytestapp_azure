import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../login/auth.service';
import { Role, Roles } from '../shared/constants/common.constant';
import { passwordMatchValidator } from '../shared/validators/common.validator';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerForm: FormGroup;
  roles: Role[] = Roles;

  constructor(private fb: FormBuilder, private authService: AuthService, private toastr: ToastrService) {

    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    }, {
      validators: passwordMatchValidator
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      this.authService.register(this.registerForm.value).subscribe(res => {
        this.toastr.success('Registration successful!', '', {
          timeOut: 3000,
        });
      });
    };
  }
}
