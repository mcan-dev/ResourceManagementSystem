import { Routes } from '@angular/router';

import { Login } from './features/auth/login/login';
import { Dashboard } from './features/dashboard/dashboard';

import { EmployeeCapacity } from './features/employee-capacity/employee-capacity';
import { Projects } from './features/projects/projects';
import { CalendarComponent } from './features/calendar/calendar';
import { LeaveRequest } from './features/leave-request/leave-request';
import { Settings } from './features/settings/settings';

import { MainLayout } from './core/layout/main-layout/main-layout';

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
      {
        path: 'employee-capacity',
        component: EmployeeCapacity
      },
           {
        path: 'projects',
        component: Projects
      },
      {
        path: 'calendar',
        component: CalendarComponent
      },
      {
        path: 'leave-request',
        component: LeaveRequest
      },
      {
        path: 'settings',
        component: Settings
      }
    ]
  },

  {
    path: '**',
    redirectTo: 'login'
  }
];