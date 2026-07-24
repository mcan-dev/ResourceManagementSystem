import { WorkDay } from './work-day.model'; 
import { Leave } from './leave.model';
import { Holiday } from './holiday.model';

export interface CalendarResponse {
  workDays: WorkDay[];
  leaves: Leave[];
  holidays: Holiday[];
}