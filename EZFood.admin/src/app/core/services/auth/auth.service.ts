import { HttpClient, HttpErrorResponse, HttpParams } from "@angular/common/http";
import { Injectable, computed, inject, signal } from '@angular/core';
import { BehaviorSubject, Observable, catchError, map, of, tap, throwError } from 'rxjs';
import { environment } from "../../../../environments/environment";
import { UserRegistration } from "../../../shared/models/auth/user-registration.model";
import { RegistrationReponse } from "../../../shared/models/auth/registration-response";
import { Router } from "@angular/router";
import { ApiResponse } from "../../../shared/models/api-response/api-response.model";
import { AuthResponse } from "../../../shared/models/auth/auth-response";
import { LoginRequest } from "../../../shared/models/auth/login-request";
import { jwtDecode } from "jwt-decode";
import { DecodedToken } from "../../../shared/models/auth/decoded-token";
import { ResetPasswordRequest } from "../../../shared/models/auth/reset-password-request";
import { ChangePasswordRequest } from "../../../shared/models/auth/change-password-request";
import { AuthenticatedUser } from "../../../shared/models/auth/authtenticated-user.model";
import { User } from "../../../shared/models/user/user.model";


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private httpClient = inject(HttpClient);
  private router = inject(Router);
  private apiUrl = `${environment.apiUrl}/auth`;
  private tokenKey = 'auth_token';


  // using signals for reactive 
  private currentUser = signal < AuthenticatedUser | null >(null);
  private isAuthenticated = signal<boolean>(false);

  // Read only signals for consumption by components
  readonly currentUser$ = computed(() => this.currentUser());
  readonly isAuthenticated$ = computed(() => this.isAuthenticated());
  readonly userRoles$ = computed(() => this.currentUser()?.roles || []);


  constructor() {
    // Initialize authentication state on service creation
    this.checkAuthState();
  }

  login(credentials: LoginRequest): Observable<AuthResponse> {
    return this.httpClient.post<AuthResponse>(`${this.apiUrl}/login`, credentials)
      .pipe(
        tap(response => {
          if(response.success){
            this.handleAuthentication(response.token)
          }
        }),
        catchError(this.handleError)
      );
  }


  registerUser(user: UserRegistration): Observable<RegistrationReponse> {
    return this.httpClient.post<RegistrationReponse>(`${this.apiUrl}/register`, user)
      .pipe(
        catchError(error => {
          console.error("Registration error", error);
          const message = error.error?.error || 'Registration failed. Please try again.';
          return throwError(() => new Error(message));
        }));
  }

  changePassword(changePasswordData: ChangePasswordRequest): Observable<{ message: string }> {
    return this.httpClient.post<{ message: string }>(`${this.apiUrl}/change-password`, changePasswordData).pipe(
      catchError(this.handleError)
    );
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.currentUser.set(null);
    this.isAuthenticated.set(false);
    this.router.navigate(['/']);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getUserCode(): string  {
    return this.currentUser$()?.userCode!;
  }

  hasRole(requiredRoles: string[]): boolean {
    if (!this.isAuthenticated$()) {
      return false;
    }
    const userRoles = this.userRoles$();
    return requiredRoles.some(role => userRoles.includes(role));
  }

  hasAllRoles(requiredRoles: string[]): boolean {
    if (!this.isAuthenticated$()) {
      return false;
    }

    const userRoles = this.userRoles$();
    return requiredRoles.every(role => userRoles.includes(role));
  }

  hasValidToken(): boolean {
    const token = this.getToken();
    if (!token) return false;
    // Check if token exists and has valid JWT format (contains two dots)
    if (!token || token.split('.').length !== 3) {
      return false;
    }

    try {
      const decodedToken = jwtDecode<DecodedToken>(token);

      // Check if token is expired
      return decodedToken.exp * 1000 > Date.now();
    } catch (error) {
      console.error('Token validation error:', error);
      this.logout(); // Clear invalid token
      return false;
    }
  }

  checkAuthState(): void {
    if (this.hasValidToken()) {
      const token = this.getToken()!;
      this.handleAuthentication(token, false);
    } else {
      this.isAuthenticated.set(false);
      this.currentUser.set(null);
    }
  }

  private handleAuthentication(token: string, redirect: boolean = true) {
    // Store token
    localStorage.setItem(this.tokenKey, token);

    try {
      // Decode token to get user data
      const decodedToken = jwtDecode<DecodedToken>(token);

      let roles: string[] = [];
      if (decodedToken.roles)
      {
        roles = decodedToken.roles.split(",")
      }
      // Extract user info  from token
      const user: AuthenticatedUser = {
        email: decodedToken.sub,
        userCode: decodedToken.userCode,
        userId: decodedToken.userId,
        name: decodedToken.name,
        roles: roles,
        isKycCompleted: decodedToken.isKycCompleted,
        hasAppliedForKyc: decodedToken.hasAppliedForKyc,
        parentNodeId: decodedToken.parentNodeId,
        tokenExpiry: new Date(decodedToken.exp * 1000)
      }
      // Add optional properties if they exist
      if (decodedToken.networkNodeId) {
        user.networkNodeId = decodedToken.networkNodeId;
      }

      if (decodedToken.parentNodeId) {
        user.parentNodeId = decodedToken.parentNodeId;
      }
      this.currentUser.set(user);
      this.isAuthenticated.set(true);

      if (redirect) {
        this.router.navigate(["/dashboard"]);
      }
    } catch (error) {
      console.error('Token decode error:', error);
      this.logout();
    }
  }
  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred';

    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      console.log(error.error.Message);
      // Server-side error
      errorMessage = error.error?.Message || `Error Code: ${error.status}, Message: ${error.message}`;
    }

    return throwError(() => new Error(errorMessage));
  }

}
