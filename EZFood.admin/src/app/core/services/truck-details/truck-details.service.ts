import { Injectable, inject } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { identity, Observable } from 'rxjs';
import { OnboardingResponse, TruckDetail } from '../../../shared/models/truck-details/truck-details.model';
import { UserEvent } from '../../../shared/models/user-events/user-events.model';

@Injectable({
  providedIn: 'root'
})
export class TruckDetailsService {

  private apiUrl = `${environment.apiUrl}/truck-details`;
  private http = inject(HttpClient);

  getAll(status: number): Observable<TruckDetail[]> {
    return this.http.get<TruckDetail[]>(`${this.apiUrl}/get-onboardings/${status}`);
  }

  getUserCalendarEvents(id: string): Observable<UserEvent[]> {
    return this.http.get<UserEvent[]>(`${this.apiUrl}/get-events/${id}`);
  }

  getAactiveTrucks(): Observable<TruckDetail[]> {
    return this.http.get<TruckDetail[]>(`${this.apiUrl}/get-active-food-trucks`);
  }
  getIncomplete(): Observable<TruckDetail[]> {
    return this.http.get<TruckDetail[]>(`${this.apiUrl}/get-incomplete-onboardings`);
  }


  getStats(): Observable<number[]> {
    return this.http.get<number[]>(`${this.apiUrl}/get-onboarding-stats`);
  }

  getPendingRequests(): Observable<TruckDetail[]> {
    return this.http.get<TruckDetail[]>(this.apiUrl);
  }


  getTruckDetailById(id: string): Observable<OnboardingResponse> {
    return this.http.get<OnboardingResponse>(`${this.apiUrl}/${id}`);
  }


createAction(data: any): Observable<any> {

    return this.http.put<any>(`${this.apiUrl}/onboarding-action`, data);
  }



  getTruckDetailByIserId(id: string): Observable<TruckDetail> {
    return this.http.get<TruckDetail>(`${this.apiUrl}/${id}`);
  }
}
