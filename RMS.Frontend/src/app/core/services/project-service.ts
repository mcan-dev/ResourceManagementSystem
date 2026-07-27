import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProjectCard } from '../../features/projects/projects';

@Injectable({
  providedIn: 'root',
})
export class ProjectService {
  
  private apiUrl = 'https://localhost:7211/api/Projects'; 

  constructor(private http: HttpClient) { }

  getProjects(): Observable<ProjectCard[]> {
    return this.http.get<ProjectCard[]>(this.apiUrl);
  }

updateProject(id: number, projectData: any): Observable<any> {
  return this.http.put(`https://localhost:7211/api/projects/${id}`, projectData);
}

createProject(projectData: any): Observable<any> {
  return this.http.post('https://localhost:7211/api/projects', projectData);
}

deleteProject(id: number): Observable<any> {
  return this.http.delete(`https://localhost:7211/api/projects/${id}`);
}

  getProjectDetails(projectId: number): Observable<any> {
  return this.http.get<any>(`${this.apiUrl}/${projectId}`);
}

getAll() {
    return this.http.get('https://localhost:7211/api/Projects'); // Kendi API adresine göre düzenle
  }

}
