import { Component, Input, inject } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import {
  LockKeyhole,
  LockKeyholeOpen,
  LucideAngularModule,
  Sun,
} from 'lucide-angular';
import { provideTablerIcons, TablerIconComponent } from 'angular-tabler-icons';
import {
  IconLayoutDashboardFilled,
  IconHeartRateMonitor,
  IconMoonFilled,
  IconBellRingingFilled,
  IconCreditCardFilled,
  IconDeviceGamepad3Filled,
  IconSettingsFilled,
  IconHeadphones,
  IconSitemap,
  IconCategory,
  IconSubtask,
  IconLockPassword,
  IconBellStar,
  IconTruckDelivery,
  IconTruck,
} from 'angular-tabler-icons/icons';
import { NgFor } from '@angular/common';
import { AuthService } from '../../../../../core/services/auth/auth.service';
interface sideMenu {
  name: string;
  link: string;
  icon: any;
}
@Component({
  selector: 'admin-sidebar',
  standalone: true,
  templateUrl: './sidebar.component.html',
  imports: [LucideAngularModule, TablerIconComponent, RouterLink, NgFor, RouterLinkActive],
  providers: [
    provideTablerIcons({
      IconHeartRateMonitor,
      IconLayoutDashboardFilled,
      IconHeadphones,
      IconMoonFilled,
      IconBellRingingFilled,
      IconCreditCardFilled,
      IconDeviceGamepad3Filled,
      IconSettingsFilled,
      IconSitemap,
      IconCategory,
      IconSubtask,
      IconLockPassword,
      IconBellStar,
      IconTruck,
      IconTruckDelivery
    }),
  ],
})
export class SidebarComponent {
  // Lucide icons
  Sun = Sun;
  LockKeyhole = LockKeyhole;
  LockKeyholeOpen = LockKeyholeOpen;

  authService = inject(AuthService);

  currentUser = this.authService.currentUser$();

  // Menu added
  public menus: sideMenu[] = [
    { name: 'Home', link: '/dashboard', icon: 'heart-rate-monitor' },
    {
      name: 'User Management',
      link: '/dashboard/users',
      icon: 'subtask',
    },
    {
      name: 'Onboarding Requests',
      link: '/dashboard/onboarding-requests',
      icon: 'truck',
    },
    {
      name: 'Food Trucks',
      link: '/dashboard/food-trucks',
      icon: 'truck',
    },
    {
      name: 'Cuisine Types',
      link: '/dashboard/cuisine-types',
      icon: 'bell-star',
    },


  ];

  sideClose: boolean = false;
  @Input() handleSideClose!: (sideClose: any) => void;

  ngOnInit() {
  }

  clickEvent() {

    this.handleSideClose((this.sideClose = !this.sideClose));
  }
}
