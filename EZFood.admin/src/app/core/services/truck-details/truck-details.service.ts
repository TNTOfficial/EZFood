import { Injectable, inject } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CuisineType } from '../../../shared/models/cuisine-types/cuisine-types.model';
import { Observable } from 'rxjs';
import { CreateCuisineType } from '../../../shared/models/cuisine-types/create-cuisine-types.model';
import { UpdateCuisineType } from '../../../shared/models/cuisine-types/update-cuisine-types.model';
import { StatusRequestCuisineType } from '../../../shared/models/cuisine-types/status-request-cuisine-types';
import { OnboardingResponse, TruckDetail } from '../../../shared/models/truck-details/truck-details.model';

@Injectable({
  providedIn: 'root'
})
export class TruckDetailsService {

  private apiUrl = `${environment.apiUrl}/truck-details`;
  private http = inject(HttpClient);

  getAll(): Observable<TruckDetail[]> {
    return this.http.get<TruckDetail[]>(this.apiUrl);
  }

  getPendingRequests(): Observable<TruckDetail[]> {
    return this.http.get<TruckDetail[]>(this.apiUrl);
  }


  getTruckDetailById(id: string): Observable<OnboardingResponse> {
    return this.http.get<OnboardingResponse>(`${this.apiUrl}/${id}`);
  }




  getTruckDetailByIserId(id: string): Observable<TruckDetail> {
    return this.http.get<TruckDetail>(`${this.apiUrl}/${id}`);
  }
}
