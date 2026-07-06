# ResourceManagementSystem
┌─────────────────────────────────────────────────────────────────────┐
│                        Frontend (Angular)                           │
│ ┌──────────┬──────────┬──────────┬──────────┬────────────────────┐  │
│ │Dashboard │ Projects │ Employees│ Capacity │      Calendar      │  │
│ └──────────┴──────────┴──────────┴──────────┴────────────────────┘  │
│              Angular Material / Custom UI Components                │
└─────────────────────────────────────────────────────────────────────┘
                                │
                                │ HTTP / HTTPS
                                ▼
┌─────────────────────────────────────────────────────────────────────┐
│                 Backend (ASP.NET Core Web API)                      │
│                                                                     │
│ ┌─────────────────────────────────────────────────────────────────┐ │
│ │ API Layer                                                       │ │
│ │ • Controllers                                                   │ │
│ │ • Authentication & Authorization                                │ │
│ │ • Middleware                                                    │ │
│ ├─────────────────────────────────────────────────────────────────┤ │
│ │ Application Layer                                               │ │
│ │ • EmployeeService                                               │ │
│ │ • ProjectService                                                │ │
│ │ • ProjectTaskService                                            │ │
│ │ • CapacityService                                               │ │
│ │ • LeaveService                                                  │ │
│ │ • DTOs                                                          │ │
│ │ • Business Rules                                                │ │
│ ├─────────────────────────────────────────────────────────────────┤ │
│ │ Infrastructure Layer                                            │ │
│ │ • Repository Interfaces                                         │ │
│ │ • Repository Implementations                                    │ │
│ │ • Entity Framework Core                                         │ │
│ │ • ProjectManagementDbContext                                    │ │
│ └─────────────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────────┘
                                │
                                ▼
┌─────────────────────────────────────────────────────────────────────┐
│                    Database (PostgreSQL 17)                         │
│                          RMS_DB                                    │
└─────────────────────────────────────────────────────────────────────┘
