import { Routes } from '@angular/router';

import { Login } from './features/auth/login/login';
import { Dashboard } from './features/dashboard/dashboard';
import { roleGuard } from './core/guards/role.guard';

import { EmployeeCapacity } from './features/employee-capacity/employee-capacity';
import { Projects } from './features/projects/projects';
import { CalendarComponent } from './features/calendar/calendar';
import { LeaveRequestComponent } from './features/leave-request/leave-request';
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
        component: LeaveRequestComponent
        // Not: İleride Yönetici tarafını kilitlediğimizde buraya da guard ekleyeceğiz.
        // canActivate: [roleGuard],
        // data: { roles: ['Manager'] }
      },
      {
        path: 'settings',
        component: Settings
      },
      
      // ÇALIŞAN İZİN SAYFASI: MainLayout içine alındı (Menülerin görünmesi için) ve Guard eklendi
    {
        path: 'my-leave-requests',
        loadComponent: () => import('./features/my-leave-requests/my-leave-requests').then(m => m.MyLeaveRequests),
        title: 'İzin Taleplerim - RMS',
        canActivate: [roleGuard],
        data: { roles: ['Employee', 'Admin', 'Manager'] } // Yöneticiler de test için girebilsin
      }
    ]
  },

  // WILDCARD (**): Her zaman en sonda olmalıdır. Aksi halde altındaki rotalar çalışmaz.
  {
    path: '**',
    redirectTo: 'login'
  }
];