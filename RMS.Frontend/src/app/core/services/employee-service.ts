import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7211/api/Employee';

  // 1. Ekip Kapasitesi sayfasındaki ana tablo için
  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.apiUrl);
  }

  // 2. Görev Atama (Task Assignment) sayfasındaki Dropdown için YENİ METOT
  getEmployeesForDropdown(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/dropdown`);
  }

  createEmployee(employee: any): Observable<Employee> {
    return this.http.post<Employee>(this.apiUrl, employee);
  }
  
  updateEmployee(id: number, employee: any): Observable<Employee> {
    return this.http.put<Employee>(`${this.apiUrl}/${id}`, employee);
  }
  
  deleteEmployee(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}