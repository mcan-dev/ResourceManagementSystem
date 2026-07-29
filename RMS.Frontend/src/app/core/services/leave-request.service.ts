import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LeaveRequestService {
  private readonly http = inject(HttpClient);
  
  // Backend URL (Kendi portuna göre burayı ayarlayabilirsin, 7211 veya 7001)
  private readonly apiUrl = 'https://localhost:7211/api/LeaveRequests';

  // ==========================================
  // YÖNETİCİ (ADMIN) METOTLARI
  // ==========================================

  getAll(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  updateStatus(id: number, statusId: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}/status`, { statusId });
  }

  // ==========================================
  // ÇALIŞAN (EMPLOYEE) METOTLARI
  // ==========================================

  getMyLeaveRequests(employeeId: number): Observable<any[]> {
    const params = new HttpParams().set('employeeId', employeeId.toString());
    return this.http.get<any[]>(`${this.apiUrl}/my-requests`, { params });
  }

  createLeaveRequest(dto: any): Observable<any> {
    return this.http.post(this.apiUrl, dto);
  }
}