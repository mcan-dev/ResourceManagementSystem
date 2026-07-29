import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BehaviorSubject, Observable, catchError, finalize, map, of } from 'rxjs';
import { PageHeaderService } from '../../core/services/page-header';
import { CalendarService } from '../../core/services/calendar.service';
import { CalendarResponse } from '../../core/models/calendar-response.model';

export interface CalendarDayUI {
  date: Date;
  dayNumber: number;
  isCurrentMonth: boolean;
  isToday: boolean;
  isWeekend: boolean;
  isHoliday: boolean;
  holidayName?: string;
  leaveEmployees: string[]; // Sadece izinliler
}

@Component({
  selector: 'app-calendar',
  standalone: true,
  imports: [CommonModule, MatProgressSpinnerModule],
  templateUrl: './calendar.html',
  styleUrls: ['./calendar.scss']
})

export class CalendarComponent implements OnInit {
  constructor() { }

  private readonly calendarService = inject(CalendarService);
  private readonly pageHeaderService = inject(PageHeaderService); // <--- EKLENDİ

  private readonly isLoadingSubject = new BehaviorSubject<boolean>(false);
  readonly isLoading$: Observable<boolean> = this.isLoadingSubject.asObservable();

  private readonly errorSubject = new BehaviorSubject<string | null>(null);
  readonly error$: Observable<string | null> = this.errorSubject.asObservable();

  private readonly calendarDaysSubject = new BehaviorSubject<CalendarDayUI[]>([]);
  readonly calendarDays$: Observable<CalendarDayUI[]> = this.calendarDaysSubject.asObservable();

  currentYear = 2026;
  currentMonth = 7; 

  readonly weekDays: string[] = ['Pzt', 'Sal', 'Çar', 'Per', 'Cum', 'Cmt', 'Paz'];
  
  get monthName(): string {
    const months = ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"];
    return months[this.currentMonth - 1];
  }

  ngOnInit(): void {
    this.pageHeaderService.setTitle('Takvim'); // <--- EKLENDİ
    this.loadCalendar(this.currentYear, this.currentMonth);
  }

  // --- Navigasyon ---
  prevMonth(): void {
    this.currentMonth--;
    if (this.currentMonth < 1) {
      this.currentMonth = 12;
      this.currentYear--;
    }
    this.loadCalendar(this.currentYear, this.currentMonth);
  }

  nextMonth(): void {
    this.currentMonth++;
    if (this.currentMonth > 12) {
      this.currentMonth = 1;
      this.currentYear++;
    }
    this.loadCalendar(this.currentYear, this.currentMonth);
  }

  goToToday(): void {
    const now = new Date();
    this.currentYear = now.getFullYear();
    this.currentMonth = now.getMonth() + 1;
    this.loadCalendar(this.currentYear, this.currentMonth);
  }

  // --- Veri Yükleme ---
  private loadCalendar(year: number, month: number): void {
    this.isLoadingSubject.next(true);
    this.errorSubject.next(null);

    this.calendarService.getMonthlyCalendar(year, month).pipe(
      map((response: CalendarResponse) => this.generateGrid(year, month, response)),
      catchError((error: Error) => {
        this.errorSubject.next(error.message);
        return of([]);
      }),
      finalize(() => this.isLoadingSubject.next(false))
    ).subscribe((grid: CalendarDayUI[]) => {
      this.calendarDaysSubject.next(grid);
    });
  }

  private generateGrid(year: number, month: number, data: CalendarResponse): CalendarDayUI[] {
    const grid: CalendarDayUI[] = [];
    const monthIndex = month - 1;

    const firstDayOfMonth = new Date(year, monthIndex, 1);
    const lastDayOfMonth = new Date(year, monthIndex + 1, 0);
    const daysInMonth = lastDayOfMonth.getDate();

    const startDayOfWeek = this.getAdjustedDayOfWeek(firstDayOfMonth);

    this.fillEmptyDays(grid, startDayOfWeek);

    for (let day = 1; day <= daysInMonth; day++) {
      grid.push(this.createCalendarDay(year, monthIndex, day, data));
    }

    return grid;
  }

  private createCalendarDay(year: number, monthIndex: number, day: number, data: CalendarResponse): CalendarDayUI {
    const currentDate = new Date(year, monthIndex, day);
    const dateString = this.formatDateStr(currentDate); 
    
    // Gerçek bugünü bul
    const today = new Date();
    const isToday = currentDate.getDate() === today.getDate() && 
                    currentDate.getMonth() === today.getMonth() && 
                    currentDate.getFullYear() === today.getFullYear();

    const isWeekend = currentDate.getDay() === 0 || currentDate.getDay() === 6;
    const holiday = data.holidays.find(h => h.date === dateString);
    const leavesForDay = data.leaves.filter(l => this.isDateInLeaveRange(currentDate, l.startDate, l.endDate));

    // Sadece izinlilerin tam adını (İsim Soyisim) al ve mükerrerleri engelle
    const leaveNames = [...new Set(leavesForDay.map(l => l.employeeName))];

    return {
      date: currentDate,
      dayNumber: day,
      isCurrentMonth: true,
      isToday: isToday,
      isWeekend: isWeekend,
      isHoliday: !!holiday,
      holidayName: holiday?.name,
      leaveEmployees: leaveNames
    };
  }

  private fillEmptyDays(grid: CalendarDayUI[], emptyCount: number): void {
    for (let i = 0; i < emptyCount; i++) {
      grid.push({
        date: new Date(0),
        dayNumber: 0,
        isCurrentMonth: false,
        isToday: false,
        isWeekend: false,
        isHoliday: false,
        leaveEmployees: []
      });
    }
  }

  private getAdjustedDayOfWeek(date: Date): number {
    let day = date.getDay() - 1;
    if (day === -1) { day = 6; }
    return day;
  }

  private isDateInLeaveRange(targetDate: Date, startDateStr: string, endDateStr: string): boolean {
    const target = targetDate.getTime();
    const start = this.parseDate(startDateStr).getTime();
    const end = this.parseDate(endDateStr).getTime();
    return target >= start && target <= end;
  }

  private formatDateStr(date: Date): string {
    const y = date.getFullYear();
    const m = String(date.getMonth() + 1).padStart(2, '0');
    const d = String(date.getDate()).padStart(2, '0');
    return `${y}-${m}-${d}`;
  }

  private parseDate(dateStr: string): Date {
    const [y, m, d] = dateStr.split('-');
    return new Date(+y, +m - 1, +d);
  }
}