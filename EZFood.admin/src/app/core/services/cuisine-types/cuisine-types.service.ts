import { Injectable, inject } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CuisineType } from '../../../shared/models/cuisine-types/cuisine-types.model';
import { Observable } from 'rxjs';
import { CreateCuisineType } from '../../../shared/models/cuisine-types/create-cuisine-types.model';
import { UpdateCuisineType } from '../../../shared/models/cuisine-types/update-cuisine-types.model';
import { StatusRequestCuisineType } from '../../../shared/models/cuisine-types/status-request-cuisine-types';

@Injectable({
  providedIn: 'root'
})
export class CuisineTypesService {

  private apiUrl = `${environment.apiUrl}/cuisine-types`;
  private http = inject(HttpClient);

  getAll(): Observable<CuisineType[]> {
    return this.http.get<CuisineType[]>(this.apiUrl);
  }


  getCuisineTypesById(id: string): Observable<CuisineType> {
    return this.http.get<CuisineType>(`${this.apiUrl}/${id}`);
  }

  createCuisineType(data: CreateCuisineType): Observable<CuisineType> {

    return this.http.post<CuisineType>(this.apiUrl, data);
  }

  updateCuisineType(id: string, packtypeData: UpdateCuisineType): Observable<CuisineType> {

    return this.http.put<CuisineType>(`${this.apiUrl}/${id}`, packtypeData);
  }

  updatecuisineTypeStatus(id: string, status: boolean): Observable<void> {
    const statusData: StatusRequestCuisineType = { status };
    return this.http.patch<void>(`${this.apiUrl}/${id}/status`, statusData);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
