import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-loading-spinner',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="flex items-center justify-center">
      <div class="animate-spin rounded-full h-10 w-10 border-t-2 border-b-2 border-l-2 border-zinc-500"></div>
    </div>
  `
})
export class LoadingSpinnerComponent { }
