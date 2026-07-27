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

  getTasksByEmployeeId(employeeId: number) {
    // NOT: Backend (C#) tarafındaki Controller route'un nasılsa URL'i ona göre güncellemelisin.
    // Örneğin C# tarafında [Route("api/[controller]")] ve [HttpGet("employee/{employeeId}")] varsa:
    return this.http.get(`${this.apiUrl}/employee/${employeeId}`); 
  }

updateEmployeeProgress(taskId: number, employeeId: number, completedHours: number) {
    const payload = {
      taskId: taskId,
      employeeId: employeeId,
      completedHours: completedHours
    };
    
    // NOT: Yine C# tarafındaki endpoint'ine göre URL'i (örneğin '/update-progress') değiştirmelisin.
    return this.http.put(`${this.apiUrl}/update-progress`, payload);
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