import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';   
import { FormsModule } from '@angular/forms'; 
import { ProjectService } from '../../core/services/project-service';
import { PageHeaderService } from '../../core/services/page-header';
import { ProjectDetail } from './project-detail/project-detail';

export interface ProjectTask {
  id: number;
  taskName: string;
  startDate: string;
  endDate: string;
  status: string;
}

export interface ProjectCard {
  id: number;
  projectName: string;
  projectDescription: string;
  startDate: string;
  endDate: string;
  statusName: string;
  priorityName: string;
  taskCount: number;
  memberCount: number;
  completedTaskCount: number;
  progressPercentage: number;
  tasks: ProjectTask[]; 
}

@Component({
  selector: 'app-projects',
  standalone: true,
  imports: [CommonModule, FormsModule, ProjectDetail], 
  templateUrl: './projects.html',
  styleUrls: ['./projects.scss']
})
export class ProjectsComponent implements OnInit {

  isCreateModalOpen = false;
  isDetailModalOpen = false;
  selectedProject: ProjectCard | null = null;
  isEditModalOpen = false;
  editProjectData: any = {}; 
  newProjectData: any = {};

  projectList: ProjectCard[] = [];
  
  constructor(
    private projectService: ProjectService,
    private cdr: ChangeDetectorRef,
    private pageHeaderService: PageHeaderService
  ) {}

  ngOnInit(): void {
    this.loadProjects();
    this.pageHeaderService.setTitle('Proje Yönetimi');
  }

  loadProjects(): void {
    this.projectService.getProjects().subscribe({
      next: (data) => {
        this.projectList = data; 
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Projeler backendden çekilirken hata oluştu:', err);
      }
    });
  }

  getStatusClass(statusName: string): string {
    switch (statusName) {
      case 'Başlamadı': return 'status-baslamadi'; 
      case 'Planlanıyor': return 'status-planlaniyor'; 
      case 'Devam Ediyor': return 'status-devamediyor'; 
      case 'Beklemede': return 'status-beklemede'; 
      case 'Test Aşamasında': return 'status-test'; 
      case 'Tamamlandı': return 'status-tamamlandi'; 
      case 'İptal Edildi': return 'status-iptal'; 
      default: return 'status-default';
    }
  }

  getPriorityClass(priorityName: string): string {
    switch (priorityName) {
      case 'Kritik Öncelik': return 'priority-kritik'; 
      case 'Yüksek Öncelik': return 'priority-yuksek'; 
      case 'Orta Öncelik': return 'priority-orta'; 
      case 'Düşük Öncelik': return 'priority-dusuk'; 
      case 'Bitti': return 'priority-bitti'; 
      default: return 'priority-belirtilmedi'; 
    }
  }
  
  openEditModal(event: Event, project: any): void {
    event.stopPropagation(); 
    
    this.editProjectData = {
      id: project.id,
      projectName: project.projectName,
      projectDescription: project.projectDescription,
      startDate: this.formatDateForInput(project.startDate),
      endDate: this.formatDateForInput(project.endDate),
      projectStatusId: project.projectStatusId || 1 
    };

    this.isEditModalOpen = true;
    this.cdr.detectChanges();
  }

  closeEditModal(): void {
    this.isEditModalOpen = false;
  }

  formatDateForInput(dateString: string): string {
    if (!dateString) return '';
    const d = new Date(dateString);
    const month = ('0' + (d.getMonth() + 1)).slice(-2);
    const day = ('0' + d.getDate()).slice(-2);
    return `${d.getFullYear()}-${month}-${day}`;
  }

  saveEditedProject(): void {
    const updateDto = {
      projectName: this.editProjectData.projectName,
      projectDescription: this.editProjectData.projectDescription,
      startDate: this.editProjectData.startDate,
      endDate: this.editProjectData.endDate,
      projectStatusId: Number(this.editProjectData.projectStatusId)
    };

    this.projectService.updateProject(this.editProjectData.id, updateDto).subscribe({
      next: (response) => {
        console.log('Proje başarıyla güncellendi', response);
        this.closeEditModal();
        this.loadProjects(); 
      },
      error: (err) => {
   const errorMessage = err.error?.message || err.error?.Message || 'Proje güncellenirken bir hata oluştu.';
  
  alert(errorMessage);
      }
    });
  }

 
  openCreateModal(): void {
    this.newProjectData = {
      projectName: '',
      projectDescription: '',
      projectStatusId: 1, 
      startDate: '',
      endDate: ''
    };
    this.isCreateModalOpen = true;
    this.cdr.detectChanges();
  }

  closeCreateModal(): void {
    this.isCreateModalOpen = false;
  }

  goToProjectDetails(project: ProjectCard): void {
    this.selectedProject = project; 
    this.isDetailModalOpen = true;  
    this.projectService.getProjectDetails(project.id).subscribe({
      next: (detailedData) => {
        this.selectedProject = detailedData;
        console.log("C#'tan Gelen Detaylı Veri:", detailedData);
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error("Proje detayları çekilirken hata oluştu:", err);
      }
    });
  }

  closeDetailModal(): void {
    this.isDetailModalOpen = false;
    this.selectedProject = null;
  }

 deleteProject(event: Event, projectId: number): void {
    event.stopPropagation(); 
    
    if (confirm('Bu projeyi silmek istediğinize emin misiniz?')) {
      // Önce API'ye silme isteğini atıyoruz
      this.projectService.deleteProject(projectId).subscribe({
        next: () => {
          console.log(`Proje (ID: ${projectId}) veritabanından başarıyla silindi.`);
          // Veritabanından başarıyla silindikten sonra UI'dan da kaldırıyoruz
          this.projectList = this.projectList.filter(p => p.id !== projectId);
          this.cdr.detectChanges(); // Ekranın anında güncellenmesini garantiliyoruz
        },
        error: (err) => {
          console.error('Silme işlemi sırasında hata oluştu:', err);
          
          // Daha önce yaptığımız gibi backend hatalarını yakalıyoruz
          let errorMessage = 'Proje silinirken bir hata oluştu.';
          if (err.error?.Message) {
            errorMessage = err.error.Message;
          } else if (err.error?.message) {
            errorMessage = err.error.message;
          } else if (typeof err.error === 'string') {
            errorMessage = err.error;
          }
          
          alert(errorMessage);
        }
      });
    }
  }

  createNewProject(): void {
    // Backend'in beklediği DTO formatını hazırlıyoruz
    const createDto = {
      projectName: this.newProjectData.projectName,
      projectDescription: this.newProjectData.projectDescription,
      startDate: this.newProjectData.startDate,
      endDate: this.newProjectData.endDate,
      projectStatusId: Number(this.newProjectData.projectStatusId)
    };

   
    this.projectService.createProject(createDto).subscribe({
      next: (response) => {
        console.log('Yeni proje başarıyla oluşturuldu:', response);
        this.closeCreateModal(); // Modalı kapat
        this.loadProjects(); // Ekrandaki listeyi anında yenile
      },
      error: (err) => {
        console.error('Proje oluşturma hatası:', err);
        
        let errorMessage = 'Proje oluşturulurken bir hata meydana geldi.';

        
        if (err.error) {
          if (err.error.Message) {
            errorMessage = err.error.Message;
          } else if (err.error.message) {
            errorMessage = err.error.message;
          } else if (err.error.errors) {
            const firstErrorKey = Object.keys(err.error.errors)[0];
            errorMessage = err.error.errors[firstErrorKey][0];
          } else if (typeof err.error === 'string') {
            errorMessage = err.error;
          }
        }
        
        alert(errorMessage);
      }
    });
  }
}