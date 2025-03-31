import { Component } from '@angular/core';
import {
  BadgeCheck,
  BadgePlus,
  Check,
  ChevronDown,
  ChevronRight,
  CircleArrowOutDownLeft,
  CircleArrowOutUpRight,
  Contact,
  FolderKanban,
  ImagePlus,
  LucideAngularModule,
  Phone,
  Search,
  X,
} from 'lucide-angular';
import { NgFor } from '@angular/common';
import { UsersComponent } from '../users/users.component';
interface dashCards {
  id: number;
  country: string;
  balance: string;
}

interface profiles {
  id: number;
  name: string;
  image: string;
}
@Component({
  selector: 'app-dashboard',
  standalone: true,
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
  imports: [
    LucideAngularModule
  ],
})
export class DashboardComponent {
  CircleArrowOutUpRight = CircleArrowOutUpRight;
  CircleArrowOutDownLeft = CircleArrowOutDownLeft;
  ChevronRight = ChevronRight;
  BadgePlus = BadgePlus;
  FolderKanban = FolderKanban;
  X = X;
  Contact = Contact;
  Phone = Phone;
  ImagePlus = ImagePlus;
  BadgeCheck = BadgeCheck;
  Check = Check;
  ChevronDown = ChevronDown;
  Search = Search;

  public dashCard: dashCards[] = [
    {
      id: 1,
      country: 'USD',
      balance: '$ 18,248.44',
    },

    {
      id: 1,
      country: 'INR',
      balance: '₹ 108,302.24',
    },

    {
      id: 1,
      country: 'EUR',
      balance: '€ 18,248.44',
    },
  ];

  public recentProfiles: profiles[] = [
    {
      id: 1,
      name: 'Reet',
      image: 'user4.webp',
    },

    {
      id: 1,
      name: 'Ramneek',
      image: 'user5.webp',
    },

    {
      id: 1,
      name: 'Rahul',
      image: 'user6.webp',
    },

    {
      id: 1,
      name: 'Ranjodh',
      image: 'user7.webp',
    },

    {
      id: 1,
      name: 'Ramesh',
      image: 'user2.webp',
    },
  ];

  isModalOpen: boolean = false;

  openModal() {
    this.isModalOpen = !this.isModalOpen;
  }
}
