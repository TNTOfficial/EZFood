import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ArrowLeft, Check, KeyRound, LucideAngularModule } from 'lucide-angular';
import { ErrorComponent } from '../../../components/error.component';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-change-password',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    LucideAngularModule,
    ErrorComponent],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css'
})
export class ChangePasswordComponent {
  KeyRound = KeyRound;
  Check = Check;
  ArrowLeft = ArrowLeft;

  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  changePasswordForm: FormGroup = this.fb.group({
    currentPassword: ['', [Validators.required]],
    newPassword: ['', [Validators.required, Validators.minLength(5)]],
    confirmPassword: ['', [Validators.required]]
  }, {
    validators: this.passwordMatchValidator
  });


  isLoading = signal(false);
  errorMessage = signal('');
  successMessage = signal('');

  passwordMatchValidator(form: FormGroup) {
    const newPassword = form.get("newPassword")?.value;
    const confirmPassword = form.get("confirmPassword")?.value;

    if (newPassword !== confirmPassword) {
      form.get("confirmPassword")?.setErrors({ passwordMismatch: true });
      return { passwordMismatch: true };
    }
    return null;
  }

  onSubmit(): void {
    if (this.changePasswordForm.invalid) {
      this.changePasswordForm.markAllAsTouched();
      return;
    }

    this.isLoading.set(true);
    this.errorMessage.set('');

    const { currentPassword, newPassword } = this.changePasswordForm.value;
    // Get userCode from auth service
    const userCode = this.authService.getUserCode();
    console.log(userCode);

    if (!userCode) {
      this.errorMessage.set('User session expired. Please login again.');
      this.isLoading.set(false);
      setTimeout(() => this.router.navigate(['/login']), 2000);
      return;
    }

    this.authService.changePassword({ userCode, currentPassword, newPassword })
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: () => {
          this.successMessage.set('Password changed successfully!');
          this.authService.logout();
          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 2000);
        },
        error: (error) => {
          this.errorMessage.set(error.message || 'Failed to change password. Please try again.');
        }
      });
  }

}
