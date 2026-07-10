import { Routes } from '@angular/router';

import { Login } from './features/auth/login/login';
import { Dashboard } from './features/dashboard/dashboard';

import { Employees } from './features/employees/employees';
import { Projects } from './features/projects/projects';
import { Calendar } from './features/calendar/calendar';
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
        path: 'employees',
        component: Employees
      },
      {
        path: 'projects',
        component: Projects
      },
      {
        path: 'calendar',
        component: Calendar
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