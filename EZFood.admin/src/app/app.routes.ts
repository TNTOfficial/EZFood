import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth/auth.guard';
import { loginGuard } from './core/guards/auth/login.guard';
import { roleGuard } from './core/guards/auth/role.guard';

export const routes: Routes = [
  {
    path: 'dashboard',
    canActivate: [authGuard, roleGuard],
    loadChildren: () =>
      import('./features/admin/routing.module').then(
        (d) => d.ADMIN_ROUTES
      ),
    data: {roles:['Admin']}
  },
  {
    path: 'login',
    canActivate: [loginGuard],
    loadComponent: () =>
      import('./shared/features/auth/login/login.component').then(
        (l) => l.LoginComponent
      ),
  },

  {
    path: 'unauthorized',
    loadComponent: () =>
      import('./shared/components/unauthorized/unauthorized.component').then(
        (u) => u.UnauthorizedComponent
      ),
  },
  {
    path: '*',
    redirectTo: '/login',
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: '/login'
  }
];
