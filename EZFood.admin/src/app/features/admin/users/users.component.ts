import { CommonModule } from "@angular/common";
import { Component, OnDestroy, OnInit, inject, signal } from "@angular/core";
import { FormBuilder, FormGroup, ReactiveFormsModule } from "@angular/forms";
import { UserTableComponent } from "./components/user-table/user-table.component";
import { UserFilterComponent } from "./components/user-filter/user-filter.component";
import { PaginationComponent } from "../../../shared/components/pagination/pagination.component";
import { LoadingSpinnerComponent } from "../../../shared/components/loading-spinner/loading-spinner.component";
import { UserService } from "../../../core/services/user/user.service";
import { Router } from "@angular/router";
import { Subject, debounceTime, distinctUntilChanged, takeUntil } from "rxjs";
import { UserDetail } from "../../../shared/models/user/user-detail.model";
import { PagedResponse } from "../../../shared/models/common/paged-response.model";
import { UserParamters } from "../../../shared/models/user/user-parameters.model";



@Component({
  selector: 'app-users',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    UserTableComponent,
    UserFilterComponent,
    PaginationComponent,
    LoadingSpinnerComponent
  ],
  templateUrl: './users.component.html'
})
export class UsersComponent implements OnInit, OnDestroy {
  private userService = inject(UserService);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  private destroy$ = new Subject<void>();

  users = signal<UserDetail[]>([]);
  pagedResponse = signal<PagedResponse<UserDetail> | null>(null);
  loading = signal<boolean>(false);
  error = signal<string | null>(null);

  parameters = signal<UserParamters>({
    pageNumber: 1,
    pageSize: 10,
    sortBy: 'createdAt',
    sortDirection: 'desc'
  });

  filterForm: FormGroup = this.fb.group({
    searchTerm: ['']
  });

  ngOnInit(): void {
    this.loadUsers();
    this.setupSearchFilter();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  private setupSearchFilter(): void {
    this.filterForm.get('searchTerm')?.valueChanges
      .pipe(
        debounceTime(400),
        distinctUntilChanged(),
        takeUntil(this.destroy$)
      )
      .subscribe(value => {
        this.updateParams({ searchTerm: value, pageNumber: 1 });
      });
  }

  loadUsers(): void {
    this.loading.set(true);
    this.error.set(null);

    this.userService.getUsers(this.parameters())
      .subscribe({
        next: (response) => {
          this.users.set(response.items);
          this.pagedResponse.set(response);
          this.loading.set(false);
        },
        error: (err) => {
          this.error.set(err.message || 'Failed to load users');
          this.loading.set(false);
        }
      });
  }

  onPageChange(page: number): void {
    this.updateParams({ pageNumber: page })
  }

  onSortChange(sortData: { sortBy: string; sortDirection: string }): void {
    this.updateParams({
      sortBy: sortData.sortBy,
      sortDirection: sortData.sortDirection
    });
  }

  viewUserDetails(userId: string): void {
    this.router.navigate(['/dashboard/users', userId]);
  }


  private updateParams(newParams: Partial<UserParamters>): void {
    this.parameters.update(params => ({ ...params, ...newParams }));
    this.loadUsers();
  }
}
