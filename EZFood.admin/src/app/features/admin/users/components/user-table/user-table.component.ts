import { Component, Input, Output, EventEmitter } from '@angular/core';
import { UserDetail } from '../../../../../shared/models/user/user-detail.model';
import { RouterModule } from '@angular/router';
import { CommonModule, DatePipe } from '@angular/common';
import { UserStatusBadgeComponent } from '../user-status-badge/user-status-badge.component';

@Component({
  selector: 'app-user-table',
  standalone:true,
  imports: [
    CommonModule,
    RouterModule,
    UserStatusBadgeComponent,
    DatePipe
  ],
  templateUrl: './user-table.component.html',
  styleUrl: './user-table.component.css'
})
export class UserTableComponent {
  @Input() users: UserDetail[] = []
  @Output() viewUser = new EventEmitter<string>();

  onViewUser(userId: string): void {
    this.viewUser.emit(userId);
  }

  getUserLocationInfo(user: UserDetail): string {
    const cityName = user.city?.name || '';
    const stateName = user.city?.state?.name || '';
    const country = user.country || '';
    const parts = [cityName, stateName, country].filter(part => part.trim() !== '');
    return parts.join(', ');
  }
}
