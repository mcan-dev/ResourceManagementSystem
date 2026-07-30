/*
import { Component, OnInit, inject, DestroyRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';



// Noktaların sayısı senin klasör derinliğine göre değişebilir
import { LeaveRequestService } from '../../core/services/leave-request.service';
import { LeaveRequestModel } from '../../core/models/leave-request.model';
import { LeaveRequestDialog } from './components/leave-request-dialog/leave-request-dialog';

@Component({
  selector: 'app-my-leave-requests',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    MatSnackBarModule,
    MatCardModule,
    MatCardModule
  ],
  templateUrl: './my-leave-requests.html',
  styleUrls: ['./my-leave-requests.scss']
})
export class MyLeaveRequests implements OnInit {
  // Dependency Injections (inject() fonksiyonu ile temiz kullanım)
  private readonly leaveRequestService = inject(LeaveRequestService);
  private readonly dialog = inject(MatDialog);
  private readonly snackBar = inject(MatSnackBar);
  private readonly destroyRef = inject(DestroyRef); // RxJS memory leak önlemi (Angular 16+ Best Practice)

  // UI State Yönetimi (Loading, Error, Empty State için)
  isLoading = true;
  hasError = false;

  // Material Table Ayarları
  // Ekranda göstereceğimiz kolonlar: Başlangıç, Bitiş, Tip, Durum ve Oluşturulma Tarihi
  displayedColumns: string[] = ['startDate', 'endDate', 'leaveTypeName', 'statusName', 'createdAt'];
  
  // Tipe sıkı sıkıya bağlı (LeaveRequestModel) MatTableDataSource
  dataSource = new MatTableDataSource<LeaveRequestModel>([]);

  // Şimdilik test için statik bir ID atıyoruz. Gerçekte Auth Service'den alınır.
  private readonly currentEmployeeId = 1;

  ngOnInit(): void {
    this.loadLeaveRequests();
  }

  loadLeaveRequests(): void {
    // İstek başlamadan önce UI'ı Loading moduna alıp Hata durumunu sıfırlıyoruz
    this.isLoading = true;
    this.hasError = false;

    this.leaveRequestService.getMyLeaveRequests(this.currentEmployeeId)
      .pipe(
        // Component destroy olduğunda (sayfa değiştiğinde vs.) Observable'ı otomatik iptal et
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe({
        next: (data: LeaveRequestModel[]) => {
          this.dataSource.data = data;
          this.isLoading = false;
        },
        error: () => {
          this.hasError = true;
          this.isLoading = false;
          this.snackBar.open('İzin talepleri yüklenirken bir hata oluştu.', 'Kapat', { duration: 4000 });
        }
      });
  }

  openNewRequestDialog(): void {
    const dialogRef = this.dialog.open(LeaveRequestDialog, {
      width: '500px', // Modern dialog genişliği
      disableClose: true, // Kullanıcı dışarı tıklayarak yanlışlıkla kapatamasın
      autoFocus: false
    });

    // Dialog kapandığında sonucu dinle
    dialogRef.afterClosed()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((isSuccess: boolean) => {
        // Eğer içeride kayıt başarılı olup 'true' dönerse, tabloyu yenile
        if (isSuccess) {
          this.loadLeaveRequests();
        }
      });
  }
}
*/