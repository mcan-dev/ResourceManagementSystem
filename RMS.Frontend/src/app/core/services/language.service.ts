import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  private readonly storageKey = 'language';

  private currentLanguage = new BehaviorSubject<'tr' | 'en'>('tr');

  currentLanguage$ = this.currentLanguage.asObservable();

  constructor() {
    this.loadLanguage();
  }

  setLanguage(language: 'tr' | 'en'): void {

    this.currentLanguage.next(language);

    localStorage.setItem(this.storageKey, language);
  }

  getLanguage(): 'tr' | 'en' {
    return this.currentLanguage.value;
  }

  private loadLanguage(): void {

    const saved =
      localStorage.getItem(this.storageKey) as 'tr' | 'en' | null;

    if (saved) {
      this.setLanguage(saved);
    } else {
      this.setLanguage('tr');
    }
  }
}