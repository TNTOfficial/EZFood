import { Component, OnInit, inject, signal } from '@angular/core';
import { LucideAngularModule, Pencil, Plus, Trash } from 'lucide-angular';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { TruckDetailsService } from '../../../../core/services/truck-details/truck-details.service';
import { UserEvent } from '../../../../shared/models/user-events/user-events.model';
@Component({
  selector: 'app-user-events',
  imports: [CommonModule, RouterLink, LucideAngularModule],
  templateUrl: './events.component.html'
})
export class EventsComponent implements OnInit {

  // Lucide Icons >>
  Plus = Plus;
  Pencil = Pencil;
  Trash = Trash;
  // Lucide Icons <<

  private truckDetailService = inject(TruckDetailsService);
  private route = inject(ActivatedRoute);

  events = signal<UserEvent[]>([]);
  loading = signal<boolean>(true);
  processingId = signal<string | null>(null);

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loading.set(true);
    const userId = this.route.snapshot.paramMap.get('id');
    this.truckDetailService
      .getUserCalendarEvents(userId!)
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (data) => {
          this.events.set(data);
        },
        error: (error) => {
          console.error('Failed to load packtypes', error);
        },
      });
  }
}
