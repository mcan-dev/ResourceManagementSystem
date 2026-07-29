import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { MatIconModule } from '@angular/material/icon'; 
import { DashboardService } from '../../core/services/dashboard-service';
import { AdminDashboardData } from '../../core/models/dashboard-model';
import { PageHeaderService } from '../../core/services/page-header';

@Component({
  selector: 'app-dashboard',
  standalone: true, 
  imports: [CommonModule, MatIconModule], 
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.scss']
})

export class Dashboard implements OnInit {
  dashboardData: AdminDashboardData | null = null;
  isLoading = true;

  constructor(private dashboardService: DashboardService,
    private cdr: ChangeDetectorRef,
    private pageHeaderService: PageHeaderService
  ) {}

  ngOnInit(): void {
    this.fetchAdminDashboard();
    this.pageHeaderService.setTitle('Panel');
  }

 fetchAdminDashboard(): void {
    this.dashboardService.getAdminDashboard().subscribe({
      next: (res: AdminDashboardData) => { 
        this.dashboardData = res;
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: (err: any) => { 
        console.error('Dashboard verisi çekilemedi:', err);
        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });
  }

  getStatusClass(status: string): string {
    switch(status.toLowerCase()) {
      case 'devam ediyor': return 'badge-primary';
      case 'beklemede': return 'badge-warning';
      case 'başlamadı': return 'badge-secondary';
      case 'tamamlandı': return 'badge-success';
      default: return 'badge-secondary';
    }
  }

  getProgressBarColor(percentage: number): string {
    if (percentage >= 80) return '#ef4444'; 
    if (percentage >= 50) return '#f59e0b'; 
    return '#22c55e'; 
  }
}