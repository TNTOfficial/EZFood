import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, Plus, Search } from 'lucide-angular';

interface SortOption {
  label: string;
  value: string;
}

@Component({
  selector: 'app-user-filter',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    LucideAngularModule
  ],
  templateUrl: './user-filter.component.html',
  styleUrl: './user-filter.component.css'
})
export class UserFilterComponent {
  Search = Search
  Plus = Plus;
  // Lucide Icon End
  @Input() form!: FormGroup;
  @Output() sortChange = new EventEmitter<{ sortBy: string; sortDirection: string }>();

  sortOptions: SortOption[] = [
    { label: 'Name A-Z', value: 'name:asc' },
    { label: 'Name Z-A', value: 'name:desc' },
    { label: 'Newest First', value: 'createdAt:desc' },
    { label: 'Oldest First', value: 'createdAt:asc' },
    { label: 'Email A-Z', value: 'email:asc' },
    { label: 'User Code', value: 'userCode:asc' },
    { label: 'Status', value: 'status:asc' }
  ];

  onSortChange(event: Event): void {
    const select = event.target as HTMLSelectElement;
    const [sortBy, sortDirection] = select.value.split(":");
    this.sortChange.emit({
      sortBy,
      sortDirection
    });
  }
}
