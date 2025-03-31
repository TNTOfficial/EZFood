import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";
import { inject } from "@angular/core";
import { AuthService } from "../../services/auth/auth.service";

export const authGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService?.isAuthenticated$()) {
    return true;
  }
  // Navigate to the login page with return url
  router.navigate(["/login"], { queryParams: { returnUrl: state.url } });
  return false;

}
