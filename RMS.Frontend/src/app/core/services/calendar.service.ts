import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CalendarResponse } from '../models/calendar-response.model';

@Injectable({
  providedIn: 'root'
})
export class CalendarService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'https://localhost:7211/api/Calendar';

  getMonthlyCalendar(year: number, month: number): Observable<CalendarResponse> {
    const params = new HttpParams()
      .set('year', year.toString())
      .set('month', month.toString());

    return this.http.get<CalendarResponse>(this.apiUrl, { params }).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'Bilinmeyen bir hata oluştu.';
    
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Hata: ${error.error.message}`;
    } else {
      errorMessage = `Sunucu Kodu: ${error.status}\nMesaj: ${error.message}`;
    }
    
    console.error('CalendarService Hatası:', errorMessage);
    return throwError(() => new Error('Takvim verileri yüklenirken bir sorun oluştu. Lütfen daha sonra tekrar deneyin.'));
  }
}