import { Component, OnInit, inject, signal } from '@angular/core';
import { Files, LucideAngularComponent, LucideAngularModule, Pencil, Plus, Trash } from 'lucide-angular';
import { CuisineTypesService } from '../../../core/services/cuisine-types/cuisine-types.service';
import { ToastService } from '../../../core/services/common/toast/toast.service';
import { CuisineType } from '../../../shared/models/cuisine-types/cuisine-types.model';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ToastContainerComponent } from '../../../shared/components/toast-container/toast-container.component';
import { TruckDetail } from '../../../shared/models/truck-details/truck-details.model';
import { TruckDetailsService } from '../../../core/services/truck-details/truck-details.service';
import { OnboardingStatus } from '../../../shared/enums/onboardingStatus';

@Component({
  selector: 'app-truck-details',
  imports: [CommonModule, RouterLink, LucideAngularModule, ToastContainerComponent],
  templateUrl: './truck-details.component.html'
})
export class TruckDetailsComponent implements OnInit {
 
  // Lucide Icons >>
  Plus = Plus;
  Pencil = Pencil;
  Files = Files;
  Trash = Trash;
  // Lucide Icons <<

  private truckDetailService = inject(TruckDetailsService);
  private toastService = inject(ToastService);

  truckDetails = signal<TruckDetail[]>([]);
  loading = signal<boolean>(true);
  processingId = signal<string | null>(null);

  ngOnInit(): void {
    this.loadData();
  }

  getStatus(key: number): string {
    return OnboardingStatus[key]; 
  }

  loadData(): void {
    this.loading.set(true);
    this.truckDetailService
      .getAll()
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (data) => {
          this.truckDetails.set(data);
        },
        error: (error) => {
          console.error('Failed to load truck details', error);
        },
      });
  }
}
