import { Component, ElementRef, OnInit, ViewChild, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { LucideAngularModule, Undo2 } from 'lucide-angular';
import { CuisineTypesService } from '../../../../core/services/cuisine-types/cuisine-types.service';
import { ToastService } from '../../../../core/services/common/toast/toast.service';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { ToastContainerComponent } from '../../../../shared/components/toast-container/toast-container.component';

@Component({
  selector: 'app-packtype-form',
  imports: [LucideAngularModule, CommonModule, RouterLink, ReactiveFormsModule, ToastContainerComponent],
  templateUrl: './cuisinetype-form.component.html'
})
export class CuisineTypeFormComponent implements OnInit {
  // Lucide Icons >>
  Undo2 = Undo2;
  // Lucide Icons <<

  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>;

  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private packtypesService = inject(CuisineTypesService);
  private toastService = inject(ToastService);
  packtypeId = signal<string | null>(null);
  isEditMode = signal<boolean>(false);
  loading = signal<boolean>(false);
  submitting = signal<boolean>(false);

  packtypeForm!: FormGroup;

    ngOnInit(): void {
      this.initForm();
      this.checkEditMode();
  }

  private initForm(): void {
    this.packtypeForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(30)]],
      description: ['', Validators.maxLength(200)],
      status: [true],
    });
  }

  private checkEditMode(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.packtypeId.set(id);
      this.isEditMode.set(true);
      this.loadpacktypesData(id);

  
    }
  }

  private loadpacktypesData(id: string): void {
    this.loading.set(true);
    this.packtypesService
      .getCuisineTypesById(id)
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (packtypes) => {
          this.packtypeForm.patchValue({
            name: packtypes.name,
            description: packtypes.description,
            status: packtypes.status,
          });
        },
        error: (error) => {
          console.error('Failed to load cuisine types data', error);
          this.toastService.error('Error', 'Failed to load cuisine types data');
          this.router.navigate(['/dashboard/cuisine-types']);
        },
      });
  }

  onSubmit(): void {
    if (this.packtypeForm.invalid) {
      Object.keys(this.packtypeForm.controls).forEach((key) => {
        this.packtypeForm.get(key)?.markAsTouched();
      });
      return;
    }

    this.submitting.set(true);
    const formData = this.packtypeForm.value;
    if (this.isEditMode()) {
      const updateData = {
        name: formData.name,
        description: formData.description,
        status: formData.status,
      };
      this.packtypesService
        .updateCuisineType(this.packtypeId()!, updateData)
        .pipe(finalize(() => this.submitting.set(false)))
        .subscribe({
          next: () => {
            this.toastService.success('Success', 'Cuisine types updated successfully');
            this.router.navigate(['/dashboard/cuisine-types']);
          },
          error: (error) => {
            console.error('Failed to update cuisine type', error);
            this.toastService.error(
              'Error',
              'Failed to update cuisine type. Please try again.'
            );
          },
        });
    } else {
      const createData = {
        name: formData.name,
        description: formData.description,
        status: formData.status,
      };

      this.packtypesService
        .createCuisineType(createData)
        .pipe(finalize(() => this.submitting.set(false)))
        .subscribe({
          next: () => {
            this.toastService.success('Success', 'Cuisine type created successfully');
            this.router.navigate(['/dashboard/cuisine-types']);
          },
          error: (error) => {
            console.error('Failed to create cuisine type', error);
            this.toastService.error(
              'Error',
              'Failed to create cuisine type. Please try again.'
            );
          },
        });
    }
  }



}
