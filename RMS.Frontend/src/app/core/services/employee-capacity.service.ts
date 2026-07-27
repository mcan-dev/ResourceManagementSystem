import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TeamCapacity } from '../models/team-capacity';
import { EmployeeWorkload } from '../models/employee-workload';


@Injectable({
  providedIn: 'root'
})
export class EmployeeCapacityService {
  TEST = 'hello';

  private apiUrl = 'https://localhost:7211/api/EmployeeCapacity';

  constructor(private http: HttpClient) { }

  getTeamSummary(): Observable<TeamCapacity[]> {
    return this.http.get<TeamCapacity[]>(`${this.apiUrl}/team-summary`);
  }
  getWorkloads(): Observable<EmployeeWorkload[]> {
  return this.http.get<EmployeeWorkload[]>(
    `${this.apiUrl}/workloads`
  );
}
}