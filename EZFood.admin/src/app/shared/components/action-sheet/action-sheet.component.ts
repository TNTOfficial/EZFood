import { NgClass } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { LucideAngularModule, Mail, Plus } from 'lucide-angular';
import { Subject, debounceTime } from 'rxjs';

@Component({
  selector: 'app-action-sheet',
  imports: [LucideAngularModule, NgClass],
  templateUrl: './action-sheet.component.html',
  standalone: true
})
export class ActionSheetComponent implements OnInit {
  @Input() action: Subject<boolean> = new Subject();
  @Input() title: string = "Title";
  @Input() subTitle?: string;
  @Input() classList?: string;
  Plus = Plus;
  Mail = Mail
  isOpenSheet: boolean = false;
  private isProcessing: boolean = false; 

  ngOnInit() {
   
    this.action.pipe(
      debounceTime(300) 
    ).subscribe(v => {
      this.sheetOpen(v);
    });
  }

  sheetOpen(action: boolean = true) {
    
    if (this.isProcessing) return;

    this.isProcessing = true;
    this.isOpenSheet = action;

    setTimeout(() => {
      this.isProcessing = false;
    }, 500);
  }
}
