import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { LucideAngularModule, ShieldAlert } from 'lucide-angular';

@Component({
  selector: 'app-unauthorized',
  standalone: true,
  imports: [LucideAngularModule],
  template: `
    <div class="flex flex-col items-center justify-center min-h-[100dvh] px-4 bg-gray-50 dark:bg-zinc-900"> 
      <div class="max-w-md w-full p-8 bg-white dark:bg-zinc-950 rounded-xl shadow-lg text-center"> 
        <div class="mb-6 flex justify-center"> 
          <lucide-icon  
            [name]="ShieldAlert"  
            size="64"  
            class="text-red-500" 
            strokeWidth="1.5"> 
          </lucide-icon> 
        </div> 
        <h1 class="text-2xl font-bold text-gray-900 dark:text-white mb-3">Access Denied</h1> 
        <p class="text-gray-600 dark:text-gray-300 mb-6"> 
          You do not have the required permissions to access this page.  
          Please contact your administrator if you believe this is an error. 
        </p> 
        <div class="flex justify-center"> 
          <button 
            (click)="onClick()" 
            class="px-4 py-2 bg-tree-500 dark:bg-river-800 text-white rounded-md hover:opacity-90 transition-opacity"> 
            Back to Login 
          </button> 
        </div> 
      </div> 
    </div> 
  `,
  styles: [`
    :host { 
      display: block; 
    } 
  `]
})
export class UnauthorizedComponent {
  ShieldAlert = ShieldAlert;
  private router = inject(Router);

  onClick(): void {
    localStorage.removeItem("auth_token");
    this.router.navigate(["/login"]);
  }
}
