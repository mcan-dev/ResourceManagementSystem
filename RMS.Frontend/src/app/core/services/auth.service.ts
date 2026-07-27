import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequest } from '../models/login.model';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
    

  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7211/api/Auth';

  ping(): Observable<string> {
    return this.http.get(`${this.apiUrl}/ping`, {
      responseType: 'text'
    });
  }

  login(data: LoginRequest) {
  return this.http.post(
    `${this.apiUrl}/login`,
    data
  );
}
}