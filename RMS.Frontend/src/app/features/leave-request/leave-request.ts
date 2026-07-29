import { Component, OnInit, inject, DestroyRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BehaviorSubject, finalize } from 'rxjs';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { PageHeaderService } from '../../core/services/page-header';
import { LeaveRequestService } from '../../core/services/leave-request.service';
import { LeaveRequestAdminModel } from '../../core/models/leave-request-admin.model';

@Component({
  selector: 'app-leave-request',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './leave-request.html',
  styleUrls: ['./leave-request.scss']
})

export class LeaveRequestComponent implements OnInit {
  
  private readonly leaveRequestService = inject(LeaveRequestService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly pageHeaderService = inject(PageHeaderService); // <--- EKLENDİ

  private readonly leaveRequestsSubject = new BehaviorSubject<LeaveRequestAdminModel[]>([]);
  readonly leaveRequests$ = this.leaveRequestsSubject.asObservable();

  private readonly isLoadingSubject = new BehaviorSubject<boolean>(false);
  readonly isLoading$ = this.isLoadingSubject.asObservable();

  readonly displayedColumns: string[] = [
    'employeeName',
    'leaveTypeName',
    'dateRange',
    'statusName',
    'actions'
  ];

  ngOnInit(): void {
    this.pageHeaderService.setTitle('İzin Yönetimi'); // <--- YORUM KALDIRILDI VE AKTİF EDİLDİ
    this.loadLeaveRequests();
  }

  loadLeaveRequests(): void {
    this.isLoadingSubject.next(true);

    this.leaveRequestService.getAll()
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        finalize(() => this.isLoadingSubject.next(false))
      )
      .subscribe({
        next: (data) => this.leaveRequestsSubject.next(data),
        error: (err) => console.error('Hata:', err)
      });
  }

  approveRequest(id: number): void {
    this.updateRequestStatus(id, 2);
  }

  rejectRequest(id: number): void {
    this.updateRequestStatus(id, 3);
  }

  private updateRequestStatus(id: number, statusId: number): void {
    this.isLoadingSubject.next(true);
    
    this.leaveRequestService.updateStatus(id, statusId)
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        finalize(() => this.isLoadingSubject.next(false))
      )
      .subscribe({
        next: () => this.loadLeaveRequests(),
        error: (err) => console.error('Hata:', err)
      });
  }

  // İsim ve soyismin ilk harflerini almak için (Örn: "Deniz Koç" -> "DK")
  getInitials(name: string): string {
    if (!name) return '';
    const parts = name.split(' ');
    if (parts.length === 1) return parts[0].substring(0, 2).toUpperCase();
    return (parts[0][0] + parts[parts.length - 1][0]).toUpperCase();
  }
}
