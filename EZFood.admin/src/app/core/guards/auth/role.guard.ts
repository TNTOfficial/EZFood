import { inject } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";
import { AuthService } from "../../services/auth/auth.service";



export const roleGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const requiredRoles = route.data['roles'] as Array<string>;

  if (!authService.isAuthenticated$()) {
    router.navigate(['/login']);
    return false;
  }
  if (authService.hasRole(requiredRoles)) {
    return true;
  }
  router.navigate(['/unauthorized']);
  return false;
};


export const allRolesGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const requiredRoles = route.data['roles'] as Array<string>;

  if (authService.isAuthenticated$() && authService.hasAllRoles(requiredRoles)) {
    return true;
  }

  router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
  return false;
};
