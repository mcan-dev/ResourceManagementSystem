import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {

  private readonly storageKey = 'theme';

  private currentTheme = new BehaviorSubject<string>('light');

  currentTheme$ = this.currentTheme.asObservable();

  constructor() {
    this.loadTheme();
  }

setTheme(theme: 'light' | 'dark'): void {

  this.currentTheme.next(theme);

  localStorage.setItem(this.storageKey, theme);

  document.body.classList.remove('light-theme', 'dark-theme');
  document.body.classList.add(`${theme}-theme`);
}

  getTheme(): string {
    return this.currentTheme.value;
  }

  toggleTheme(): void {

    const newTheme =
      this.currentTheme.value === 'light'
        ? 'dark'
        : 'light';

    this.setTheme(newTheme);
  }

  private loadTheme(): void {

    const savedTheme =
      localStorage.getItem(this.storageKey) as 'light' | 'dark' | null;

    if (savedTheme) {
      this.setTheme(savedTheme);
    } else {
      this.setTheme('light');
    }
  }
}