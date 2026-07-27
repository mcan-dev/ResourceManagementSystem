import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ManagerTask } from '../models/manager-task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskAssignmentService {
  
  private apiUrl = 'https://localhost:7211/api/TaskAssignments'; 

  constructor(private http: HttpClient) { }

  createTaskWithAssignments(payload: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/create-with-assignments`, payload);
  }
  
deleteTask(taskId: number) {
    return this.http.delete(`https://localhost:7211/api/TaskAssignments/${taskId}`);
  }

  getAllManagerTasks(): Observable<ManagerTask[]> {
    return this.http.get<ManagerTask[]>(`${this.apiUrl}/employee/manager`);
  }

  addSingleAssignment(payload: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/add-assignment`, payload);
  }

  updateAssignmentHours(id: number, hours: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/update-hours/${id}`, { assignedHours: hours });
  }

  deleteSingleAssignment(assignmentId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/assignment/${assignmentId}`);
  }
}