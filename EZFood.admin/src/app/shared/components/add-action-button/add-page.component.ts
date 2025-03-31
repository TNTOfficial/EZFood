import { Component, OnInit, Input, Output, EventEmitter, HostListener, OnDestroy } from '@angular/core';
import { ConfirmationService } from '../../services/confirmation/confirmation.service';
import { UntypedFormGroup } from '@angular/forms';
import { debounceTime, Subject, Subscription } from 'rxjs';

@Component({
  selector: 'app-add-page',
  templateUrl: './add-page.component.html'
})
export class AddPageBtnComponent implements OnInit, OnDestroy {

  @Output() addAction: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() cancelAction: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Input() rForm: UntypedFormGroup;
  @Input() disable: boolean;             
  @Input() isPopUp = true;             
  @Input() btnText = ["add", "addAndContinue", "cancel"];
  @Input() btnShow = [true, false, true];
  private debounceTime = 800;
  private clicks = new Subject<boolean>();
  private subscription: Subscription;
  private runOnce: boolean = true;

  constructor(private confirmationService: ConfirmationService) {
  }

  ngOnInit() {
    this.subscription = this.clicks.pipe(
      debounceTime(this.debounceTime)
    ).subscribe(e => { this.runOnce = true; this.onAdd(e); });
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
  onBack() {
    this.confirmationService.backUrl;
  }

  onAdd(act = false) {
    if ((this.btnShow[0] || this.btnShow[1]) && !this.disable) {
      this.addAction.emit(act);
    }
  }

  onCancel(act = false) {
    this.cancelAction.emit(false);
  }
  @HostListener('window:keydown', ['$event'])
  onKeyDown(event: KeyboardEvent) {
    if ((event.metaKey || event.altKey) && event.key.toLowerCase() === 's') { /* ALT + s*/
      event.preventDefault(); 
      if (this.runOnce) {
        this.runOnce = false;
        this.clicks.next(false);
      }
               
    } else if (event.shiftKey && event.keyCode === 27 && this.btnShow[1]) { /* Shift + ESC */
      event.preventDefault();
      if (this.runOnce) {
        this.runOnce = false;
        this.clicks.next(true);
      }
  }
  else if(event.keyCode === 27) { /* ESC */
      event.preventDefault();
      this.onCancel();
    }
  }

}
