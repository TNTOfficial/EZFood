import { Injectable, inject } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { HttpClient, HttpParams } from "@angular/common/http";
import { User } from "../../../shared/models/user/user.model";
import { Observable } from "rxjs";
import { UserParamters } from "../../../shared/models/user/user-parameters.model";
import { UserDetail } from "../../../shared/models/user/user-detail.model";
import { PagedResponse } from "../../../shared/models/common/paged-response.model";
import { UserUpdate } from "../../../shared/models/user/user-update-model";

@Injectable({
  providedIn: "root"
})
export class UserService {
  private apiUrl = `${environment.apiUrl}/users`;
  private http = inject(HttpClient);

  getUsers(parameters: UserParamters): Observable<PagedResponse<UserDetail>> {
    const { pageNumber, pageSize, sortBy, sortDirection, searchTerm } = parameters;
    let url = `${this.apiUrl}?pageNumber=${pageNumber}&pageSize=${pageSize}&sortBy=${sortBy}&sortDirection=${sortDirection}`;

    if (searchTerm) {
      url += `&searchTerm=${searchTerm}`;
    }

    return this.http.get<PagedResponse<UserDetail>>(url);
  }

  // Get a specific user's details
  getUserDetails(userId: string): Observable<UserDetail> {
    return this.http.get<UserDetail>(`${this.apiUrl}/${userId}`)
  }

  createUser(user: Partial<UserDetail>): Observable<UserDetail> {
    return this.http.post<UserDetail>(this.apiUrl, user);
  }

  updateUser(adminUserId: string, userId: string, user: UserUpdate): Observable<UserUpdate> {
    return this.http.put<UserUpdate>(`${this.apiUrl}/admin/${adminUserId}/user/${userId}`, user);
  }

  deleteUser(userId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${userId}`);
  }

  // Get user by code
  getUserByCode(userCode: string): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/by-code/${userCode}`);
  }

  // Search users in hierarchy
  searchUsers(query: string): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/search`, { params: { query } })
  }

}
