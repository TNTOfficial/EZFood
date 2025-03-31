import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import {
  BellDot,
  LucideAngularModule,
  MessageSquareText,
  Moon,
  Power,
  ScanFace,
  Settings,
  Sun,
} from 'lucide-angular';
import {  } from 'angular-tabler-icons';
import { AuthService } from '../../../../../core/services/auth/auth.service';
@Component({
  selector: 'app-admin-header',
  standalone: true,
  templateUrl: './admin-header.component.html',

  imports: [LucideAngularModule,  RouterLink],
})
export class AdminHeaderComponent {
  // Lucide icons
  BellDot = BellDot;
  MessageSquareText = MessageSquareText;
  Settings = Settings;
  Power = Power;
  ScanFace = ScanFace;
  Moon = Moon;
  Sun = Sun;

  isDarkMode = signal(false);
  authService = inject(AuthService);

  themeService = this;

  toggleDarkMode() {
    this.isDarkMode.update((value) => !value);
    document.documentElement.classList.toggle('dark', this.isDarkMode());
  }

  isDropdownOpen: boolean = false;

  showMenu() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  showMenuIn() {
    this.isDropdownOpen = true;
  }
  
  showMenuOut() {
    this.isDropdownOpen = false;
  }

  logout(): void {
    this.authService.logout();
  }
}
