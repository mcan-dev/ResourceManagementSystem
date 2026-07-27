import { Routes } from '@angular/router';
import { MainLayout } from './core/layout/main-layout/main-layout';
import { Dashboard } from './features/dashboard/dashboard';
import { ProjectsComponent } from './features/projects/projects';
import { Login } from './features/auth/login/login';
import { Employees } from './features/employees/employees';
import { EmployeeCapacity } from './features/employee-capacity/employee-capacity';
import { CalendarComponent } from './features/calendar/calendar';
import { LeaveRequestComponent } from './features/leave-request/leave-request';
import { Settings } from './features/settings/settings';
import { Tasks } from './features/tasks/tasks';



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
        path: 'calendar',
        component: CalendarComponent
      },
      {
      path: 'tasks', // <-- Rota adının sidebar'daki routerLink="/tasks" ile birebir aynı olması gerekir
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