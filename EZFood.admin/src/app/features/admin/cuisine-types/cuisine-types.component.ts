import { Component, OnInit, inject, signal } from '@angular/core';
import { LucideAngularComponent, LucideAngularModule, Pencil, Plus, Trash } from 'lucide-angular';
import { CuisineTypesService } from '../../../core/services/cuisine-types/cuisine-types.service';
import { ToastService } from '../../../core/services/common/toast/toast.service';
import { CuisineType } from '../../../shared/models/cuisine-types/cuisine-types.model';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ToastContainerComponent } from '../../../shared/components/toast-container/toast-container.component';

@Component({
  selector: 'app-pack-types',
  imports: [CommonModule, RouterLink, LucideAngularModule, ToastContainerComponent],
  templateUrl: './cuisine-types.component.html'
})
export class CuisineTypesComponent implements OnInit {
 
  // Lucide Icons >>
  Plus = Plus;
  Pencil = Pencil;
  Trash = Trash;
  // Lucide Icons <<

  private packtypeService = inject(CuisineTypesService);
  private toastService = inject(ToastService);

  cuisineTypes = signal<CuisineType[]>([]);
  loading = signal<boolean>(true);
  processingId = signal<string | null>(null);

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loading.set(true);
    this.packtypeService
      .getAll()
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (data) => {
          this.cuisineTypes.set(data);
        },
        error: (error) => {
          console.error('Failed to load packtypes', error);
        },
      });
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this cuisine type?')) {
      this.processingId.set(id);

      this.packtypeService
        .delete(id)
        .pipe(finalize(() => this.processingId.set(null)))
        .subscribe({
          next: () => {
            // Remove the packtype from the list
            const updatedPacktypes = this.cuisineTypes().filter((s) => s.id !== id);
            this.cuisineTypes.set(updatedPacktypes);
            this.toastService.success('Success', 'Cuisine type deleted successfully');
          },
          error: (error) => {
            console.error('Failed to delete cuisine type', error);
            this.toastService.error(
              'Error',
              'Failed to delete cuisine type. Please try again.'
            );
          },
        });
    }
  }

}
