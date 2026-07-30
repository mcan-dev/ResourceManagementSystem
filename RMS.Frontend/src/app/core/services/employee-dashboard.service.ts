import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeDashboardData } from '../models/employee-dashboard-model';

@Injectable({
  providedIn: 'root'
})
@Injectable({
  providedIn: 'root'
})
export class EmployeeDashboardService {
  
  private apiUrl = 'https://localhost:7211/api'; 

  constructor(private http: HttpClient) {}

  getEmployeeDashboard(employeeId: number): Observable<EmployeeDashboardData> {
    return this.http.get<EmployeeDashboardData>(`${this.apiUrl}/Dashboard/employee/${employeeId}`);
  }

  /*  Buraya ve ya yeni bir emplooye_leave_request_service oluşturulup onla çalıştırılacak */
}