import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LeaveRequestAdminModel } from '../models/leave-request-admin.model';

@Injectable({
  providedIn: 'root'
})
export class LeaveRequestService {
  private readonly http = inject(HttpClient);
  
  // API URL'sini .NET Core projenin çalıştığı 7211 portuna sabitledik
  private readonly apiUrl = 'https://localhost:7211/api/LeaveRequests';

  getAll(): Observable<LeaveRequestAdminModel[]> {
    return this.http.get<LeaveRequestAdminModel[]>(`${this.apiUrl}/admin-list`);
  }

  updateStatus(id: number, statusId: number): Observable<{ message: string }> {
    return this.http.patch<{ message: string }>(`${this.apiUrl}/${id}/status`, { statusId });
  }
}