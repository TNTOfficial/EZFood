import {
  Component,
  Input,
  Output,
  EventEmitter,
  ChangeDetectionStrategy,
  Inject,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PdfViewerModule } from 'ng2-pdf-viewer'; // <- import PdfViewerModule
import { LoadingSpinnerComponent } from '../loading-spinner/loading-spinner.component';
import { Download, LucideAngularModule, X } from 'lucide-angular';

@Component({
  selector: 'app-pdf-viewer',
  templateUrl: './pdf-viewer.component.html',
  imports: [FormsModule, PdfViewerModule, LoadingSpinnerComponent, LucideAngularModule],
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PdfViewerComponent {

  X = X;
  Download = Download;

  @Input() isVisible: boolean | null = false;
  @Input() dataUrl: string | null = null;
  @Output() closeModal = new EventEmitter<void>();
  isLoading = true;
  src =
    'http://143.198.101.124:5000/uploads/documents/DCHCertificate/DCHCertificate_638816969613587093.pdf';

  onClose(): void {
    console.log(this.dataUrl);

    this.closeModal.emit();
  }

  onPdfLoadComplete() {
    this.isLoading = false;
  }
  onError(error: any) {
    this.isLoading = false;
    console.error('PDF loading error:', error);
  }
}
