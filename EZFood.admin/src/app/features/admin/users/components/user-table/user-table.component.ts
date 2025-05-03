import { Component, Input, Output, EventEmitter } from '@angular/core';
import { UserDetail } from '../../../../../shared/models/user/user-detail.model';
import { RouterModule } from '@angular/router';
import { CommonModule, DatePipe } from '@angular/common';
import { UserStatusBadgeComponent } from '../user-status-badge/user-status-badge.component';
import { GalleryVerticalEnd, LucideAngularModule } from 'lucide-angular';

@Component({
  selector: 'app-user-table',
  standalone:true,
  imports: [
    CommonModule,
    RouterModule,
    UserStatusBadgeComponent,
    DatePipe,
    LucideAngularModule
  ],
  templateUrl: './user-table.component.html',
  styleUrl: './user-table.component.css'
})
export class UserTableComponent {

  GalleryVerticalEnd = GalleryVerticalEnd;

  @Input() users: UserDetail[] = []
  @Output() viewUser = new EventEmitter<string>();

  onViewUser(userId: string): void {
    this.viewUser.emit(userId);
  }

}
