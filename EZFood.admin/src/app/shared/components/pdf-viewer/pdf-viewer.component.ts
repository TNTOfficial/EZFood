import {
  Component,
  Input,
  Output,
  EventEmitter,
  ChangeDetectionStrategy,
  Inject,
} from '@angular/core';
import {
  NgxExtendedPdfViewerModule,
  NgxExtendedPdfViewerService,
} from 'ngx-extended-pdf-viewer';

@Component({
  selector: 'app-pdf-viewer',
  templateUrl: './pdf-viewer.component.html',
  imports: [NgxExtendedPdfViewerModule],
  providers: [NgxExtendedPdfViewerService],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PdfViewerComponent {
  @Input() isVisible: boolean | null = false;
  @Input() dataUrl: string | null = null;
  @Output() closeModal = new EventEmitter<void>();
  private pdfService: NgxExtendedPdfViewerService = Inject(NgxExtendedPdfViewerService);

  onClose(): void {
    console.log(this.dataUrl);

    this.closeModal.emit();
  }
}
