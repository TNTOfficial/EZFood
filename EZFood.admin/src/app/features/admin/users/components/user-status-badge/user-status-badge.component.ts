import { Component, Input } from "@angular/core";
import { UserStatus } from "../../../../../shared/models/enums/user-status";

@Component({
  selector: 'app-user-status-badge',
  standalone: true,
  imports: [],
  template: `
  <span [class]="statusClasses" class="px-2 py-1 text-xs font-medium rounded">
  {{getStatusText()}}
  </span>

  `
})
export class UserStatusBadgeComponent {
  @Input() status: UserStatus = UserStatus.Active;

  get statusClasses(): string {
    switch (this.status) {
      case UserStatus.Active:
        return 'bg-green-100 text-green-800';
      case UserStatus.Inactive:
        return 'bg-yellow-100 text-yellow-800';
      default:
        return 'bg-gray-100 text gray-800';
    }
  }

  getStatusText(): string {
    switch (this.status) {
      case UserStatus.Active:
        return 'Active';
      case UserStatus.Inactive:
        return 'Inactive';
      default:
        return 'Unknown';
    }
  }
}
