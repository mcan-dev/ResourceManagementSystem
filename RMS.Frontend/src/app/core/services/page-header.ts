import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PageHeaderService {
  // Varsayılan başlığımız boş veya Panel olabilir
  private titleSubject = new BehaviorSubject<string>(''); 
  title$ = this.titleSubject.asObservable();

  setTitle(newTitle: string) {
    this.titleSubject.next(newTitle);
  }
}