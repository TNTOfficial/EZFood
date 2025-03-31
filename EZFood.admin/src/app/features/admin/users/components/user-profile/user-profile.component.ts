import { Component, OnDestroy, OnInit, inject, signal } from '@angular/core';
import { UserService } from '../../../../../core/services/user/user.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { UserDetail } from '../../../../../shared/models/user/user-detail.model';
import { RegistrationType } from '../../../../../shared/models/enums/registration-type';
import { CommonModule, DatePipe } from '@angular/common';
import { UserStatusBadgeComponent } from '../user-status-badge/user-status-badge.component';
import { LoadingSpinnerComponent } from '../../../../../shared/components/loading-spinner/loading-spinner.component';

@Component({
  selector: 'app-user-profile',
  imports: [
    CommonModule,
    RouterModule,
    LoadingSpinnerComponent
  ],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit, OnDestroy {
  private userService = inject(UserService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private destroy$ = new Subject<void>();

  user = signal<UserDetail | null>(null);
  loading = signal<boolean>(true);
  error = signal<string | null>(null);
  registrationType = RegistrationType;

  ngOnInit():void {
    this.loadUserDetails()
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public loadUserDetails(): void {
    const userId = this.route.snapshot.paramMap.get("id");
    if (!userId) {
      this.error.set('User ID is missing');
      this.loading.set(false);
      return;
    }

    this.userService.getUserDetails(userId).pipe(takeUntil(this.destroy$)).subscribe({
      next: (user: any) => {
        this.user.set(user as UserDetail);
        this.loading.set(false);
      },
      error: (err) => {
        this.error.set(err.message || 'Failed to load user details');
        this.loading.set(false);
      }
    });
  }

  getUserLocationInfo(): string {
    if (!this.user()) return '';

    const user = this.user()!;
    const cityName = user.city?.name || '';
    const stateName = user.city?.state?.name || '';
    const country = user.country || '';

    const parts = [cityName, stateName, country].filter(part => part.trim() !== '');
    return parts.join(', ');
  }
  editUser(): void {
    if (this.user()) {
      this.router.navigate(['/dashboard/users', this.user()!.id, 'edit']);
    }
  }


  goBack(): void {
    this.router.navigate(['/dashboard/users']);
  }
}
