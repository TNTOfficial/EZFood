import { Component, OnDestroy, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { CommonModule, DatePipe } from '@angular/common';
import { LoadingSpinnerComponent } from '../../../../shared/components/loading-spinner/loading-spinner.component';
import { TruckDetailsService } from '../../../../core/services/truck-details/truck-details.service';
import { OnboardingResponse, TruckDetail } from '../../../../shared/models/truck-details/truck-details.model';
import { OnboardingStatus } from '../../../../shared/enums/onboardingStatus';

@Component({
  selector: 'app-user-profile',
  imports: [
    CommonModule,
    RouterModule,
    LoadingSpinnerComponent
  ],
  templateUrl: './truck-detail.component.html',
})
export class TruckDetailComponent implements OnInit, OnDestroy {
  private truckDetailService = inject(TruckDetailsService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private destroy$ = new Subject<void>();

  truckDetail = signal<OnboardingResponse | null>(null);
  loading = signal<boolean>(true);
  error = signal<string | null>(null);

  ngOnInit():void {
    this.loadDetails()
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public loadDetails(): void {
    const truckId = this.route.snapshot.paramMap.get("id");
    if (!truckId) {
      this.error.set('Truck id is missing');
      this.loading.set(false);
      return;
    }

    this.truckDetailService.getTruckDetailById(truckId).pipe(takeUntil(this.destroy$)).subscribe({
      next: (truckDetail: OnboardingResponse) => {
        this.truckDetail.set(truckDetail);
        this.loading.set(false);
      },
      error: (err) => {
        this.error.set(err.message || 'Failed to load user details');
        this.loading.set(false);
      }
    });
  }

   getStatus(key: number): string {
      return OnboardingStatus[key];
    }

  goBack(): void {
    this.router.navigate(['/dashboard/truck-details']);
  }
}
