import { Component, OnInit, inject } from '@angular/core';
import { Check, KeyRound, LucideAngularModule, Mail } from 'lucide-angular';
import { Router } from '@angular/router';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { ErrorComponent } from '../../../components/error.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    LucideAngularModule,
    ErrorComponent,
  ],
})
export class LoginComponent implements OnInit {
  Mail = Mail;
  KeyRound = KeyRound;
  Check = Check;

  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  loginForm: FormGroup = this.fb.group({
    email: ['', Validators.required],
    password: ['', Validators.required],
    rememberMe: [false],
  });

  isLoading = false;
  errorMessage = '';

  ngOnInit() {
    // Check if user should be remembered
    const rememberedUser = localStorage.getItem('rememberUser');
    if (rememberedUser) {
      this.loginForm.patchValue({
        userCode: rememberedUser,
        rememberMe: true,
      });
    }
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    const { email, password, rememberMe } = this.loginForm.value;

    this.authService.login({ email, password: password }).subscribe({
      next: (response) => {
        if (!response.success) {
          this.errorMessage = 'Invalid credentials. Please try again.';
        } else {
          // Store remember me preference if selected
          if (rememberMe) {
            localStorage.setItem('rememberUser', email);
          } else {
            localStorage.removeItem('rememberUser');
          }
          this.router.navigate(['/dashboard']);
        }
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage =
          error.message || 'Invalid credentials. Please try again.';
      },
      complete: () => {
        this.isLoading = false;
      },
    });
  }
}
