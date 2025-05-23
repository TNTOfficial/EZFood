import { Routes } from "@angular/router";
import { AdminLayoutComponent } from "../../shared/layouts/admin-layout/admin-layout.component";

export const ADMIN_ROUTES: Routes = [
  {
    path: "",
    component: AdminLayoutComponent,
    children: [
      {
        path: "",
        loadComponent: () =>
          import('../admin/dashboard/dashboard.component').then(
            (d) => d.DashboardComponent
          ),
      },
      {
        path: "users",
        children: [
          {
            path: "",
            loadComponent: () => import("./users/users.component").then(u => u.UsersComponent),
            title: 'User Management'
          },
          {
            path: ':id',
            loadComponent: () => import('./users/components/user-profile/user-profile.component').then(c => c.UserProfileComponent),
            title: 'User Profile'
          },
          {
            path: 'events/:id',
            loadComponent: () => import('./users/events/events.component').then(c => c.EventsComponent),
            title: 'User Profile'
          },
        ]
      },
      {
        path: "cuisine-types",
        children: [
          {
            path: "",
            loadComponent: () => import("../admin/cuisine-types/cuisine-types.component").then(
              p => p.CuisineTypesComponent)
          },
          {
            path: "create",
            loadComponent: () => import("./cuisine-types/cuisine-type-form/cuisinetype-form.component")
              .then(p => p.CuisineTypeFormComponent)

          },
          {
            path: "edit/:id",
            loadComponent: () => import("./cuisine-types/cuisine-type-form/cuisinetype-form.component").then(
              p => p.CuisineTypeFormComponent
            )
          }

        ]

      },
      {
        path: "onboarding-requests",
        children: [
          {
            path: "",
            loadComponent: () => import("../admin/truck-details/truck-details.component").then(
              p => p.TruckDetailsComponent)
          },
          {
            path: "details/:id",
            loadComponent: () => import("./truck-details/detail/truck-detail.component").then(
              p => p.TruckDetailComponent
            )
          }

        ]
      },
      {
        path: "food-trucks",
        children: [
          {
            path: "",
            loadComponent: () => import("../admin/food-trucks/food-trucks.component").then(
              p => p.FoodTrucksComponent)
          },
          {
            path: "details/:id",
            loadComponent: () => import("./food-trucks/detail/food-truck-detail.component").then(
              p => p.FoodTruckDetailComponent
            )
          }

        ]
      },
    ]
  }
]
