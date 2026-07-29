import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskAssignmentService } from '../../core/services/task-assignment.service';
import { FormsModule } from '@angular/forms';
import { EmployeeService } from '../../core/services/employee-service';
import { ProjectService } from '../../core/services/project-service';
import { PageHeaderService } from '../../core/services/page-header';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './tasks.html',
  styleUrl: './tasks.scss',
})
export class Tasks implements OnInit {
  
  tasks: any[] = []; 
  userRole: string = ''; 
  
  // --- YENİ EKLENEN DEĞİŞKENLER (Çalışan Ekranı İçin) ---
  userName: string = ''; 
  newCompletedHours: number | null = null; 

  isNewTaskModalOpen: boolean = false;

  projects: any[] = [];
  employees: any[] = [];  

  newTaskPayload = {
    projectId: 0,
    taskName: '',
    startDate: '',
    endDate: '',
    taskStatus: 'Başlamadı',
    assignments: [] as any[]
  };

  tempAssignment = {
    employeeId: 0,
    assignedHours: null as number | null
  };

  isManageModalOpen: boolean = false;
  selectedManageTask: any = null;
  newManageAssignment = { 
    employeeId: 0, 
    assignedHours: null as number | null 
  };

  constructor(
    private taskService: TaskAssignmentService, 
    private employeeService: EmployeeService,
    private projectService: ProjectService,
    private cdr: ChangeDetectorRef,
    private pageHeaderService: PageHeaderService
  ) {}

  ngOnInit(): void {
    // KULLANICI ROLÜNÜ, İSMİNİ VE ID'SİNİ YAKALIYORUZ
    this.userRole = localStorage.getItem('userRole') || '';
    this.userName = localStorage.getItem('userName') || localStorage.getItem('fullName') || 'Bilinmeyen Kullanıcı'; // İsmi güvenli şekilde aldık
     this.pageHeaderService.setTitle('Görev & Atama');
    const userIdStr = localStorage.getItem('userId');
    const userId = userIdStr ? Number(userIdStr) : 0;

    this.loadDropdownData();

    // ROLE GÖRE VERİ ÇEKME İŞLEMİ
    if (this.userRole === 'Sistem Yöneticisi' || this.userRole === 'Proje Yöneticisi') {
      this.loadManagerTasks();
    } else {
      this.loadEmployeeTasks(userId);
    }
  }

 loadDropdownData() {
    this.projectService.getAll().subscribe({
      next: (data: any) => {
        console.log('Gelen Projeler:', data); 
        this.projects = data;
        this.cdr.detectChanges();
      },
      error: (err: any) => console.error('Projeler çekilemedi:', err)
    });

    this.employeeService.getEmployeesForDropdown().subscribe({
      next: (data: any) => {
        console.log('Gelen Personeller:', data); 
        this.employees = data;
        this.cdr.detectChanges();
      },
      error: (err: any) => console.error('Personeller çekilemedi:', err)
    });
  }
  
  loadManagerTasks(): void {
    this.taskService.getAllManagerTasks().subscribe({
      next: (data: any) => {
        console.log('Gelen Yönetici Görev Verisi:', data);
        this.tasks = data;
        this.cdr.detectChanges();
      },
      error: (err: any) => {
        console.error('Yönetici görevleri yüklenirken hata:', err);
      }
    }); 
  }

  // ÇALIŞANLARA ÖZEL GÖREV GETİRME METODU EKLENDİ
  loadEmployeeTasks(employeeId: number): void {
    if (employeeId === 0) {
      console.error('Geçersiz Personel ID. Giriş yapılmamış olabilir.');
      return;
    }

    this.taskService.getTasksByEmployeeId(employeeId).subscribe({
      next: (data: any) => {
        console.log('Gelen Personel Görev Verisi:', data);
        this.tasks = data;
        this.cdr.detectChanges();
      },
      error: (err: any) => {
        console.error('Personel görevleri yüklenirken hata:', err);
      }
    });
  }

  resetForm() {
    this.newTaskPayload = {
      projectId: 0,
      taskName: '',
      startDate: '',
      endDate: '',
      taskStatus: 'Başlamadı',
      assignments: []
    };
    this.tempAssignment = { employeeId: 0, assignedHours: null };
  }

  openNewTaskModal() { 
    this.resetForm(); 
    this.isNewTaskModalOpen = true; 
  }

  closeNewTaskModal() { 
    this.isNewTaskModalOpen = false; 
  }

  addAssignment() {
    if (this.tempAssignment.employeeId != 0 && this.tempAssignment.assignedHours != null && this.tempAssignment.assignedHours > 0) {
      const selectedEmployee = this.employees.find(e => e.id === this.tempAssignment.employeeId);
      const employeeName = selectedEmployee ? selectedEmployee.fullName : 'Bilinmeyen Personel';

      this.newTaskPayload.assignments.push({ 
        ...this.tempAssignment, 
        fullName: employeeName 
      });

      this.tempAssignment = { employeeId: 0, assignedHours: null }; 
    } else {
      alert("Lütfen hem bir personel seçtiğinizden hem de çalışma saatini doğru girdiğinizden emin olun.");
    }
  }

  removeAssignment(index: number) {
    this.newTaskPayload.assignments.splice(index, 1);
  }

  submitTask() {
    console.log("Backend'e giden veri (Payload):", this.newTaskPayload);

    this.taskService.createTaskWithAssignments(this.newTaskPayload).subscribe({
      next: (response: any) => {
        console.log('Başarıyla kaydedildi!', response);
        this.closeNewTaskModal(); 
        
        // Kayıt sonrası güncel veriyi çekerken de rol kontrolü yapıyoruz
        if (this.userRole === 'Sistem Yöneticisi' || this.userRole === 'Proje Yöneticisi') {
          this.loadManagerTasks();  
        } else {
          const userIdStr = localStorage.getItem('userId');
          if(userIdStr) this.loadEmployeeTasks(Number(userIdStr));
        }
      },
      error: (err: any) => {
        console.error('Kayıt hatası:', err);
        alert('Kaydedilirken hata oluştu.');
      }
    });
  }

  // --- GÖREV SİLME (TÜM GÖREVİ) ---
  deleteTask(taskId: number) {
    if (!taskId) {
      console.error('HATA: taskId boş veya undefined geldi!');
      return; 
    }

    const isConfirmed = confirm('Bu görevi ve bağlı olan tüm personel atamalarını silmek istediğinize emin misiniz?');

    if (isConfirmed) {
      this.taskService.deleteTask(taskId).subscribe({
        next: (res) => {
          console.log('Silme başarılı!', res);
          this.loadManagerTasks(); // Çalışanlar silemeyeceği için sadece yönetici listesini yenilemek yeterli
        },
        error: (err) => {
          console.error('Silinirken hata oluştu:', err);
          alert('Görev silinirken bir hata oluştu.');
        }
      });
    }
  }

  // --- GÖREV YÖNETİM MODALI METOTLARI ---

  openManageModal(task: any) {
  this.selectedManageTask = { 
      ...task, 
      assignees: task.assignees ? [...task.assignees] : [] 
    }; 
    this.isManageModalOpen = true;
  }

  closeManageModal() {
    this.isManageModalOpen = false;
    this.selectedManageTask = null;
    this.newManageAssignment = { employeeId: 0, assignedHours: null };
    this.newCompletedHours = null; // Modalı kapatırken çalışanın inputunu da temizliyoruz
  }

  updateAssignedHours(assignment: any) {
    console.log("Seçilen Atama Objesi:", assignment);

    // Backend'den dönen ID alanının tam adını yakalamaya çalışıyoruz
    const targetId = assignment.id || assignment.assignmentId || assignment.taskAssignmentId;

    if (!targetId) {
      console.error("HATA: Bu atamanın bir ID değeri yok!", assignment);
      alert("Hata: Atama ID'si bulunamadı. F12 Konsolunu kontrol edin.");
      return;
    }

    if (!assignment.assignedHours || assignment.assignedHours <= 0) {
      alert('Lütfen geçerli bir saat giriniz.');
      return;
    }

    this.taskService.updateAssignmentHours(targetId, assignment.assignedHours).subscribe({
      next: (res) => {
        console.log('Saat başarıyla güncellendi (Backend Yanıtı):', res);
        this.loadManagerTasks(); 
      },
      error: (err) => {
        console.error('Backend Hatası Detayı:', err);
        
        if (err.status === 404) {
          alert('Hata: Kayıt veritabanında bulunamadı (404).');
        } else if (err.status === 400) {
          alert('Hata: Gönderilen veri formatı hatalı (400).');
        } else if (err.status === 0) {
          alert('Hata: Sunucuya ulaşılamıyor veya CORS hatası! (0)');
        } else {
          alert(`Saat güncellenemedi. Durum Kodu: ${err.status}`);
        }
      }
    });
  }

  // --- YENİ EKLENEN METOT: ÇALIŞANIN KENDİ İLERLEMESİNİ GÜNCELLEMESİ ---
  updateMyProgress() {
    // 1. Girilen saatin geçerliliğini kontrol et
    if (this.newCompletedHours === null || this.newCompletedHours < 0) {
      alert('Lütfen geçerli bir tamamlanan saat giriniz.');
      return;
    }

    // 2. Görev ID'sini ve Kullanıcı ID'sini yakala
    const currentTaskId = this.selectedManageTask.taskId || this.selectedManageTask.id; 
    const currentUserId = Number(localStorage.getItem('userId'));

    // 3. Servise isteği gönder
    this.taskService.updateEmployeeProgress(currentTaskId, currentUserId, this.newCompletedHours).subscribe({
      next: (response: any) => {
        console.log('İlerleme başarıyla güncellendi:', response);
        this.newCompletedHours = null; // Input alanını sıfırla
        this.closeManageModal(); // İşlem bitince modalı kapat
        
        // Ekrana güncel veriyi yansıtmak için listeyi tekrar çekiyoruz
        this.loadEmployeeTasks(currentUserId); 
      },
      error: (err: any) => {
        console.error('İlerleme kaydedilirken hata oluştu:', err);
        alert('Saat güncellenemedi, lütfen tekrar deneyin.');
      }
    });
  }

  removeTaskAssignment(assignmentId: number) {
    const isConfirmed = confirm('Bu personeli görevden çıkarmak istediğinize emin misiniz?');
    if (isConfirmed) {
      this.taskService.deleteSingleAssignment(assignmentId).subscribe({
        next: (res) => {
          console.log('Personel görevden çıkarıldı');
          // Arayüzden personeli anında düşür ki ekran hemen güncellensin
          this.selectedManageTask.assignees = this.selectedManageTask.assignees.filter((a: any) => a.id !== assignmentId);
          this.loadManagerTasks(); 
        },
        error: (err) => {
          console.error('Silme hatası:', err);
          alert('Personel görevden çıkarılamadı.');
        }
      });
    }
  }

  addNewTaskAssignment() {
    if (!this.newManageAssignment.employeeId || !this.newManageAssignment.assignedHours) {
      alert('Lütfen personel ve saat seçin!');
      return;
    }

    const payload = {
      taskId: this.selectedManageTask.taskId, 
      employeeId: this.newManageAssignment.employeeId,
      assignedHours: this.newManageAssignment.assignedHours
    };

    this.taskService.addSingleAssignment(payload).subscribe({
      next: (res) => {
        console.log('Yeni personel atandı:', res);
        this.newManageAssignment = { employeeId: 0, assignedHours: null };
        this.loadManagerTasks(); // Ana tabloyu güncelle
        this.closeManageModal(); // Modalı kapat
      },
      error: (err) => {
        console.error('Atama hatası:', err);
        alert('Personel ataması yapılamadı.');
      }
    });
  }

  // --- YARDIMCI METOTLAR ---

  // HTML'deki Slice hatasını çözen metot
  getInitials(name: string): string {
    if (!name) return '??'; 
    return name.substring(0, 2).toUpperCase(); 
  }

  getEmployeeName(id: number): string {
    const employee = this.employees.find(e => e.id === id); 
    return employee ? employee.fullName : 'Bilinmeyen Personel'; 
  }

  getStatusClass(status: string | undefined | null): string {
    switch (status?.toLowerCase()) {
      case 'tamamlandı': return 'status-completed';
      case 'devam ediyor': return 'status-in-progress';
      case 'beklemede': return 'status-pending';
      case 'başlamadı': return 'status-not-started';
      default: return 'status-default';
    }
  }
}