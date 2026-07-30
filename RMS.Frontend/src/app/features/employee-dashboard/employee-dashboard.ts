import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { EmployeeDashboardService } from '../../core/services/employee-dashboard.service';
import { EmployeeDashboardData } from '../../core/models/employee-dashboard-model';
import { PageHeaderService } from '../../core/services/page-header';

@Component({
  selector: 'app-employee-dashboard',
  standalone: true,
  imports: [CommonModule, MatIconModule],
  templateUrl: './employee-dashboard.component.html',
  styleUrls: ['./employee-dashboard.component.scss']
})
export class EmployeeDashboardComponent implements OnInit {
  isLoading: boolean = true;
  dashboardData!: EmployeeDashboardData;
  employeeId!: number; 

  constructor(
    private dashboardService: EmployeeDashboardService,
    private pageHeaderService: PageHeaderService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.pageHeaderService.setTitle('Panel');
    
    
    const userIdStr = localStorage.getItem('userId'); 

 if (userIdStr) {
     
      this.employeeId = Number(userIdStr);     
      console.log('Çekilen Çalışan ID:', this.employeeId);          
      this.fetchData(); 

    } 
    else {
      console.error('Kullanıcı ID LocalStorage içinde bulunamadı!');
      this.isLoading = false;
    }
  }

  fetchData(): void {
    if (!this.employeeId) return;

    this.dashboardService.getEmployeeDashboard(this.employeeId).subscribe({
      next: (res) => {
        this.dashboardData = res;
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Personel paneli verisi alınamadı:', err);
        // Hata durumunda da loading'i false yapalım ki ekran beyaz kalmasın, en azından boş halini veya hata mesajını görelim.
        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });
  }

  getStatusClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'devam ediyor': return 'badge-blue';
      case 'başlamadı': return 'badge-gray';
      case 'beklemede': return 'badge-orange';
      case 'tamamlandı': return 'badge-green';
      default: return 'badge-gray';
    }
  }
}