import { Component, OnInit, inject, signal } from '@angular/core';
import { Files,  LucideAngularModule, Pencil, Plus, Search, Trash, ListPlus, ListRestart, ListTodo, GitPullRequestClosed } from 'lucide-angular';
import { ToastService } from '../../../core/services/common/toast/toast.service';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ToastContainerComponent } from '../../../shared/components/toast-container/toast-container.component';
import { TruckDetail } from '../../../shared/models/truck-details/truck-details.model';
import { TruckDetailsService } from '../../../core/services/truck-details/truck-details.service';
import { OnboardingStatus } from '../../../shared/enums/onboardingStatus';
import { LoadingSpinnerComponent } from "../../../shared/components/loading-spinner/loading-spinner.component";
import { getOnboardingStatus } from '../../../shared/utils/helper';

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
  ListPlus = ListPlus;
  ListRestart = ListRestart;
  ListTodo = ListTodo;
  GitPullRequestClosed = GitPullRequestClosed;
  // Lucide Icons <<

  private truckDetailService = inject(TruckDetailsService);
  private toastService = inject(ToastService);

  truckDetails = signal<TruckDetail[]>([]);
  stats = signal<number[]>([0,0,0,0]);
  loading = signal<boolean>(true);
  activeTab = signal<OnboardingStatus>(OnboardingStatus.Submitted);
  processingId = signal<string | null>(null);

  public get OnboardingStatus() {
    return OnboardingStatus;
  }

  ngOnInit(): void {

    this.loadFullData(OnboardingStatus.Submitted);
  }

  getStatus(key: OnboardingStatus): string {
    return getOnboardingStatus(key);
  }

  loadFullData(status: OnboardingStatus){
    this.loadStats();
    this.loadData(status);
  }

  loadStatusData(status: OnboardingStatus){
    this.activeTab.set(status);
    if(status == OnboardingStatus.Incomplete){
      this.loadIncompleteStatusData();
    } else {
      this.loadData(status);
    }
  }
  loadData(status: number): void {
    this.loading.set(true);
    this.truckDetailService
      .getAll(status)
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

  loadIncompleteStatusData(): void {
    this.loading.set(true);
    this.truckDetailService
      .getIncomplete()
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
  loadStats(): void {
    this.loading.set(true);
    this.truckDetailService
      .getStats()
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (data) => {
          this.stats.set(data);
        },
        error: (error) => {
          console.error('Failed to load truck details', error);
        },
      });
  }
}
