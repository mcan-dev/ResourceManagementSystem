import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmailChangeRequest } from '../models/email-change-request.model'; 
import { PasswordChangeRequest } from '../models/password-change-request.model';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {
  private readonly http = inject(HttpClient);
  
  private readonly apiUrl = 'https://localhost:7211/api/Settings';

  changeEmail(request: EmailChangeRequest): Observable<any> {
    return this.http.put(`${this.apiUrl}/email`, request);
  }

  changePassword(request: PasswordChangeRequest): Observable<any> {
    return this.http.put(`${this.apiUrl}/password`, request);
  }
}