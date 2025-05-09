import { Component, OnDestroy, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { finalize, Subject, takeUntil } from 'rxjs';
import { CommonModule } from '@angular/common';
import { LoadingSpinnerComponent } from '../../../../shared/components/loading-spinner/loading-spinner.component';
import { TruckDetailsService } from '../../../../core/services/truck-details/truck-details.service';
import { OnboardingResponse } from '../../../../shared/models/truck-details/truck-details.model';
import { OnboardingStatus } from '../../../../shared/enums/onboardingStatus';
import {
  ArrowLeft,
  BadgeCheck,
  ChevronDown,
  CircleCheckBig,
  CircleX,
  ClipboardCheck,
  Download,
  FileText,
  GitPullRequestClosed,
  Image,
  ListRestart,
  LucideAngularModule,
  View,
  X,
} from 'lucide-angular';
import { ImageService } from '../../../../core/services/image.service';
import { PdfViewerService } from '../../../../core/services/pdf-viewer.service';
import { PdfViewerComponent } from '../../../../shared/components/pdf-viewer/pdf-viewer.component';
import { FormBuilder, FormsModule, Validators } from '@angular/forms';
import { ToastService } from '../../../../core/services/common/toast/toast.service';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { getOnboardingStatus } from '../../../../shared/utils/helper';

@Component({
  selector: 'app-food-truck-detail',
  imports: [
    CommonModule,
    RouterModule,
    LoadingSpinnerComponent,
    LucideAngularModule,
    PdfViewerComponent,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './food-truck-detail.component.html',
})
export class FoodTruckDetailComponent implements OnInit, OnDestroy {
  ArrowLeft = ArrowLeft;
  ChevronDown = ChevronDown;
  View = View;
  X = X;
  CircleX = CircleX;
  BadgeCheck = BadgeCheck;
  Image = Image;
  FileText = FileText;
  Download = Download;
  ClipboardCheck = ClipboardCheck;
  ListRestart = ListRestart;
  GitPullRequestClosed = GitPullRequestClosed;
  CircleCheckBig = CircleCheckBig;

  // Lucide Icons<<

  public imageService = inject(ImageService);
  private truckDetailService = inject(TruckDetailsService);
  public pdfViewerService = inject(PdfViewerService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private destroy$ = new Subject<void>();
  public pdfData: string | null = null;
  public imageSRC: string | null = null;
  private toastService = inject(ToastService);
  submitting = signal<boolean>(false);

  truckDetail = signal<OnboardingResponse | null>(null);
  loading = signal<boolean>(true);
  error = signal<string | null>(null);
  private fb = inject(FormBuilder);
  public get OnboardingStatus() {
    return OnboardingStatus;
  }
  rForm!: FormGroup;

  ngOnInit(): void {
    this.loadDetails();
    this.rForm = this.fb.group({
      onboardingStatus: [OnboardingStatus.Approved],
      note: ['', Validators.maxLength(200)],
      truckDetailId: [this.route.snapshot.paramMap.get('id')]
    });

    this.rForm.get('onboardingStatus')?.valueChanges.subscribe((type) => {
      const dependentControl = this.rForm.get('note');
      if (type != OnboardingStatus.Approved) {
        dependentControl?.setValidators([Validators.required, Validators.maxLength(200)]);
      } else {
        dependentControl?.clearValidators();
      }
       dependentControl?.updateValueAndValidity();
    });
  }

  modalTitle(): string {
    return this.rForm.get('onboardingStatus')?.value == OnboardingStatus.Approved ? 'Approve' : this.rForm.get('onboardingStatus')?.value == OnboardingStatus.ReferBack ? 'Refer Back' : 'Reject';
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  checkDocType(document: string | null): string {
    if (document) {
      var docExt = document!.replace(/\.([^.]+)$/, ':;$1').split(':;');
      return docExt[1];
    } else {
      return 'null';
    }
  }

  openPdf(data: string | null) {
    console.log('pdf click');

    this.pdfData = this.imageService.getImageUrl(data);
    this.pdfViewerService.showModal();
  }
  getStatus(key: OnboardingStatus): string {
    return getOnboardingStatus(key);
  }
  submitAction() {
    console.log(this.rForm.value);
    const data = this.rForm.value;
    this.submitting.set(true);
    this.truckDetailService
      .createAction(data)
      .pipe(finalize(() => this.submitting.set(false)))
      .subscribe({
        next: (data: any) => {

          if (data.result) {
            this.truckDetail.set(data);
            this.toastService.success(
              'Success',
              'Response to onboarding request is submitted successfully.'
            );
            this.closeReactionModal();
          } else {
            this.toastService.error(
              'Error',
              'Failed to submit response. Please try again.'
            );
          }
        },
        error: (error: any) => {
          console.error('Failed to submit response', error);
          this.toastService.error(
            'Error',
            'Failed to submit response. Please try again.'
          );
        },
      });
  }

  public loadDetails(): void {
    const truckId = this.route.snapshot.paramMap.get('id');
    if (!truckId) {
      this.error.set('Truck id is missing');
      this.loading.set(false);
      return;
    }

    this.truckDetailService
      .getTruckDetailById(truckId)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (truckDetail: OnboardingResponse) => {
          this.truckDetail.set(truckDetail);
          this.loading.set(false);
        },
        error: (err) => {
          this.error.set(err.message || 'Failed to load user details');
          this.loading.set(false);
        },
      });
  }

  goBack(): void {
    this.router.navigate(['/dashboard/onboarding-requests']);
  }

  step1: boolean = false;
  step2: boolean = false;
  step3: boolean = false;
  step4: boolean = false;
  step5: boolean = false;

  stepOpen(id: number) {
    if (id == 1) {
      this.step1 = !this.step1;
    } else if (id == 2) {
      this.step2 = !this.step2;
    } else if (id == 3) {
      this.step3 = !this.step3;
    } else if (id == 4) {
      this.step4 = !this.step4;
    } else if (id == 5) {
      this.step5 = !this.step5;
    } else {
      this.step1 = false;
      this.step2 = false;
      this.step3 = false;
      this.step4 = false;
      this.step5 = false;
    }
  }

  isModalOpen: boolean = false;
  isReactionModalOpen: boolean = false;

  viewDocument(data: string) {
    this.imageSRC = data;
    this.isModalOpen = true;
  }
  closeDocument() {
    this.isModalOpen = false;
  }

  openReactionModal(type: OnboardingStatus) {
    this.rForm.setValue({
      onboardingStatus: type,
      note: '',
      truckDetailId: this.route.snapshot.paramMap.get('id')
    });
    this.isReactionModalOpen = true;
  }

  closeReactionModal() {
    this.isReactionModalOpen = false;
  }
}
