import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PdfViewerService {
  private isModalVisibleSubject = new BehaviorSubject<boolean>(false);
  isModalVisible$ = this.isModalVisibleSubject.asObservable();

  showModal() {
    this.isModalVisibleSubject.next(true);
  }

  hideModal() {
    this.isModalVisibleSubject.next(false);
  }
}
