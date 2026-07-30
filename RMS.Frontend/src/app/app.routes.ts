import { Routes } from '@angular/router';
import { MainLayout } from './core/layout/main-layout/main-layout';
import { Dashboard } from './features/dashboard/dashboard';
import { ProjectsComponent } from './features/projects/projects';
import { Login } from './features/auth/login/login';
import { EmployeeCapacity } from './features/employee-capacity/employee-capacity';
import { CalendarComponent } from './features/calendar/calendar';
import { LeaveRequestComponent } from './features/leave-request/leave-request';
import { Settings } from './features/settings/settings';
import { Tasks } from './features/tasks/tasks';
import { EmployeeDashboardComponent } from './features/employee-dashboard/employee-dashboard';


export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: Login
  },
  {
    path: '',
    component: MainLayout,
    children: [
      {
        path: 'dashboard',
        component: Dashboard
      },
      // İŞTE BURAYA TAŞIDIK! Artık yan menüyle birlikte açılacak.
      { 
        path: 'employee-dashboard', 
        component: EmployeeDashboardComponent 
      },
      {
        path: 'employee-capacity',
        component: EmployeeCapacity
      },
      {
        path: 'calendar',
        component: CalendarComponent
      },
      {
        path: 'tasks',
        component: Tasks
      },
      {
        path: 'leave-request',
        component: LeaveRequestComponent
      },
      {
        path: 'settings',
        component: Settings
      },
      {
         path: 'projects',
         component: ProjectsComponent 
      }
    ]
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];