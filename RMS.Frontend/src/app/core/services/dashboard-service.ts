import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AdminDashboardData } from '../models/dashboard-model';
import { environment } from '../../../environments/environment'; 

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
 
  private apiUrl = 'https://localhost:7211/api/dashboard'; 

  constructor(private http: HttpClient) { }

  getAdminDashboard(): Observable<AdminDashboardData> {
    return this.http.get<AdminDashboardData>(`${this.apiUrl}/admin`);
  }
}