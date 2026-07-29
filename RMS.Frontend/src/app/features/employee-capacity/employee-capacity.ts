import { Component, OnInit, inject } from '@angular/core';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { ChangeDetectorRef } from '@angular/core';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Employee } from '../../core/models/employee.model';
import { EmployeeService } from '../../core/services/employee-service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { EmployeeDialog } from './components/employee-dialog/employee-dialog';
import { EmployeeCapacityService } from '../../core/services/employee-capacity.service';
import { TeamCapacity } from '../../core/models/team-capacity';
import { EmployeeWorkload } from '../../core/models/employee-workload';
import { PageHeaderService } from '../../core/services/page-header';

@Component({
  selector: 'app-employee-capacity',
  standalone: true,
  imports: [
    MatProgressBarModule,
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatInputModule,
    MatSelectModule,
    MatIconModule,
    FormsModule,
    MatDialogModule
  ],
  templateUrl: './employee-capacity.html',
  styleUrl: './employee-capacity.scss'
})
export class EmployeeCapacity implements OnInit {
  constructor() { }

  private employeeService = inject(EmployeeService);
  private employeeCapacityService = inject(EmployeeCapacityService);
  private dialog = inject(MatDialog);
  private cdr = inject(ChangeDetectorRef);
  private pageHeaderService = inject(PageHeaderService); 

  displayedColumns: string[] = [
    'name',
    'surname',
    'teamName',
    'titleName',
    'status',
    'actions'
  ];

  dataSource = new MatTableDataSource<Employee>([]);
  teamCapacities: TeamCapacity[] = [];
  
  employeeWorkloads: EmployeeWorkload[] = []; 
  filteredWorkloads: EmployeeWorkload[] = []; 

  selectedTeamId: number | null = null;

  searchText = '';
  
  applyFilter(): void {
    this.dataSource.filter = this.searchText.trim().toLowerCase();
  }

  selectTeam(teamId: number): void {
    if (this.selectedTeamId === teamId) {
      this.selectedTeamId = null;
    } else {
      this.selectedTeamId = teamId;
    }
    this.filterWorkloadsByTeam();
  }

  filterWorkloadsByTeam(): void {
    if (this.selectedTeamId === null) {
      this.filteredWorkloads = [...this.employeeWorkloads];
    } else {
      this.filteredWorkloads = this.employeeWorkloads.filter(
        employee => employee.teamId === this.selectedTeamId
      );
    }
  }

  getCapacityClass(capacity: number): string {
    if (capacity >= 90) return 'danger';
    if (capacity >= 80) return 'warning';
    if (capacity >= 50) return 'primary';
    return 'success';
  }

  testClick(): void {
    console.log("Test Clicked");
  }

  openAddEmployeeDialog(): void {
    const dialogRef = this.dialog.open(EmployeeDialog, {
      width: '600px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadEmployees();
      }
    });
  }

  ngOnInit(): void {
    this.pageHeaderService.setTitle('Çalışan & Kapasite'); 

    this.loadEmployees();
    this.loadTeamCapacities();
    this.loadEmployeeWorkloads();

    this.dataSource.filterPredicate = (employee, filter) => {
      const text = `${employee.name} ${employee.surname} ${employee.email} ${employee.teamName} ${employee.titleName}`;
      return text.toLowerCase().includes(filter);
    };
  }

  loadEmployees(): void {
    this.employeeService.getEmployees().subscribe({
      next: (data) => {
        this.dataSource.data = data;
      },
      error: (err) => console.error(err)
    });
  }

  loadTeamCapacities(): void {
    this.employeeCapacityService.getTeamSummary().subscribe({
      next: (data) => {
        this.teamCapacities = data.map((team: any) => {
          const remainingHours = team.averageCapacity !== undefined ? team.averageCapacity : team.capacity;
          const usedHours = 80 - remainingHours;
          const percentage = Math.round((usedHours / 80) * 100);

          if (team.averageCapacity !== undefined) {
             team.averageCapacity = percentage;
          } else {
             team.capacity = percentage;
          }
          return team;
        });
        this.cdr.detectChanges();
      }
    });
  }

  editEmployee(employee: Employee): void {
    const dialogRef = this.dialog.open(EmployeeDialog, {
      width: '600px',
      data: { employee }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadEmployees();
      }
    });
  }

  deleteEmployee(employee: Employee): void {
    if (!confirm(`${employee.name} adlı çalışan silinsin mi?`)) {
      return;
    }

    this.employeeService.deleteEmployee(employee.id).subscribe({
      next: () => {
        this.loadEmployees();
      },
      error: (err) => {
        console.error('Silme hatası:', err);
      }
    });
  }

  loadEmployeeWorkloads(): void {
    this.employeeCapacityService.getWorkloads().subscribe({
      next: (data) => {
        this.employeeWorkloads = data.map(item => {
          const remainingHours = item.capacity;
          const usedHours = 80 - remainingHours;
          item.capacity = Math.round((usedHours / 80) * 100); 
          return item;
        });
        
        this.filterWorkloadsByTeam();
        this.cdr.detectChanges(); 
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  getCapacityColor(capacity: number): string {
    if (capacity >= 90) return '#ef4444';  
    if (capacity >= 0) return '#f59e0b';   
    if (capacity >= 25) return '#2c24d1';  
    return '#22c55e';  
  }
}