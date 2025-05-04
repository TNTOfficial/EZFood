import { Component, OnInit, inject, signal } from '@angular/core';
import { Files,  LucideAngularModule, Pencil, Plus, Search, Trash } from 'lucide-angular'; 
import { ToastService } from '../../../core/services/common/toast/toast.service'; 
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ToastContainerComponent } from '../../../shared/components/toast-container/toast-container.component';
import { TruckDetail } from '../../../shared/models/truck-details/truck-details.model';
import { TruckDetailsService } from '../../../core/services/truck-details/truck-details.service';
import { OnboardingStatus } from '../../../shared/enums/onboardingStatus';
import { LoadingSpinnerComponent } from "../../../shared/components/loading-spinner/loading-spinner.component";

@Component({
  selector: 'app-truck-details',
  imports: [CommonModule, RouterLink, LucideAngularModule, ToastContainerComponent, LoadingSpinnerComponent],
  templateUrl: './truck-details.component.html'
})
export class TruckDetailsComponent implements OnInit {
 
  // Lucide Icons >>
  Plus = Plus;
  Pencil = Pencil;
  Files = Files;
  Trash = Trash;
  Search = Search;
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
