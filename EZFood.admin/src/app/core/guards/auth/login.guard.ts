import { inject } from "@angular/core"
import { AuthService } from "../../services/auth/auth.service"
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";

export const loginGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  // If user is already authenticated, redirect to dashboard
  if (authService.isAuthenticated$()) {
    // Check if user has admin role
    if (authService.hasRole(['Admin'])) {
      router.navigate(['/dashboard']);
    } else {
      // This is important - if they're authenticated but not an admin,
      // they should still be able to see login
      return true;
    }
    return false;
  }

  // Not authenticated, allow access to login
  return true;
};
