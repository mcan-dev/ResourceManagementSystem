using Microsoft.EntityFrameworkCore;
using RMS.DataLayer.Entities;
using RMS.DataLayer.RmsDb;
using RMS.RepositoryLayer.Interfaces;
using RMS.ServiceLayer.DTOs;
using RMS.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
       
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<List<ProjectCardListDto>> GetProjectCardsAsync()
        {
            var projects = await _projectRepository.GetAllProjectsWithDetailsAsync();

            var bugun = DateOnly.FromDateTime(DateTime.Now);

            var projectCards = projects.Select(p =>
            {
                // --- YENİ EKLENEN KISIM: Görevlerdeki saatleri baştan topluyoruz ---
                var allAssignments = p.ProjectTasks != null
                    ? p.ProjectTasks.SelectMany(t => t.TaskAssignments ?? new List<TaskAssignment>()).ToList()
                    : new List<TaskAssignment>();

                decimal totalAssigned = allAssignments.Sum(a => a.AssignedHours != null ? Convert.ToDecimal(a.AssignedHours) : 0);
                decimal totalCompleted = allAssignments.Sum(a => a.CompletedHours != null ? Convert.ToDecimal(a.CompletedHours) : 0);
                // -------------------------------------------------------------------

                return new ProjectCardListDto
                {
                    Id = p.Id,
                    ProjectName = p.ProjectName,
                    ProjectDescription = p.ProjectDescription,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,

                    StatusName = p.ProjectStatusId == 1 ? "Başlamadı" :
                                 p.ProjectStatusId == 2 ? "Planlanıyor" :
                                 p.ProjectStatusId == 3 ? "Devam Ediyor" :
                                 p.ProjectStatusId == 4 ? "Beklemede" :
                                 p.ProjectStatusId == 5 ? "Test Aşamasında" :
                                 p.ProjectStatusId == 6 ? "Tamamlandı" :
                                 p.ProjectStatusId == 7 ? "İptal Edildi" : "Belirtilmedi",

                    PriorityId = p.EndDate.HasValue
                                 ? (p.EndDate.Value.DayNumber < bugun.DayNumber ? 5
                                  : p.EndDate.Value.DayNumber - bugun.DayNumber <= 3 ? 4
                                  : p.EndDate.Value.DayNumber - bugun.DayNumber <= 7 ? 3
                                  : p.EndDate.Value.DayNumber - bugun.DayNumber <= 14 ? 2
                                  : 1)
                                 : 0,

                    PriorityName = p.EndDate.HasValue
                                   ? (p.EndDate.Value.DayNumber < bugun.DayNumber ? "Bitti"
                                    : p.EndDate.Value.DayNumber - bugun.DayNumber <= 3 ? "Kritik Öncelik"
                                    : p.EndDate.Value.DayNumber - bugun.DayNumber <= 7 ? "Yüksek Öncelik"
                                    : p.EndDate.Value.DayNumber - bugun.DayNumber <= 14 ? "Orta Öncelik"
                                    : "Düşük Öncelik")
                                   : "Belirtilmedi",

                    TaskCount = p.ProjectTasks?.Count ?? 0,

                    MemberCount = p.ProjectTasks != null
                                  ? p.ProjectTasks.SelectMany(t => t.TaskAssignments)
                                                  .Select(ta => ta.EmployeeId)
                                                  .Distinct()
                                                  .Count()
                                  : 0,

                    ProgressPercentage = totalAssigned > 0
                                         ? (int)Math.Round((double)(totalCompleted / totalAssigned) * 100, 0)
                                         : 0
                };
            }).ToList();

            return projectCards;
        }

        public async Task<ProjectDetailDto?> GetProjectDetailsAsync(int projectId)
        {
            var project = await _projectRepository.GetProjectWithDetailsAsync(projectId);

            if (project == null) return null;

            var bugun = DateOnly.FromDateTime(DateTime.Now);

            var taskList = project.ProjectTasks?.Select(t =>
            {
                var assignment = t.TaskAssignments?.FirstOrDefault();
                decimal assignedHrs = assignment?.AssignedHours != null ? Convert.ToDecimal(assignment.AssignedHours) : 0;
                decimal completedHrs = assignment?.CompletedHours != null ? Convert.ToDecimal(assignment.CompletedHours) : 0;

                return new ProjectTaskDetailDto
                {
                    Id = t.Id,
                    TaskName = t.TaskName,
                    Deadline = t.EndDate,

                    AssigneeName = assignment != null && assignment.Employee != null
                        ? $"{assignment.Employee.Name} {assignment.Employee.Surname}"
                        : "Atanmadı",

                    AssigneeTitle = assignment != null && assignment.Employee?.Title != null
                        ? assignment.Employee.Title.TitleName
                        : "-",

                    AssignedHours = assignedHrs,
                    TotalHours = assignedHrs, 
                    CompletedHours = completedHrs,

                    ProgressPercentage = assignedHrs > 0
                ? Math.Round((double)(completedHrs / assignedHrs) * 100, 0)
                : 0
                };
            }).ToList() ?? new List<ProjectTaskDetailDto>();

            
            var detailDto = new ProjectDetailDto
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                ProjectDescription = project.ProjectDescription,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                TaskCount = taskList.Count,
                Tasks = taskList
            };

            return detailDto;
        }

        private int CalculatePriorityId(DateOnly? endDate)
        {
            if (!endDate.HasValue) return 1;

            var bugun = DateOnly.FromDateTime(DateTime.Now);
            var kalanGun = endDate.Value.DayNumber - bugun.DayNumber;

            if (kalanGun < 0) return 5;
            if (kalanGun <= 3) return 4;
            if (kalanGun <= 7) return 3;
            if (kalanGun <= 14) return 2;

            return 1;
        }

        public async Task<Project> AddProjectAsync(ProjectCreateDto projectDto)
        {

            if (projectDto.ProjectStatusId <= 0)
            {
                throw new ArgumentException("Proje durum ID'si (ProjectStatusId) zorunludur ve geçerli bir değer olmalıdır.");
            }


            if (projectDto.StartDate > projectDto.EndDate)
            {
                throw new ArgumentException("Proje başlangıç tarihi (StartDate), bitiş tarihinden (EndDate) büyük olamaz.");
            }

            var isProjectNameExists = await _projectRepository.AnyAsync(p => p.ProjectName == projectDto.ProjectName);

            if (isProjectNameExists)
            {
                throw new ArgumentException($"'{projectDto.ProjectName}' adında bir proje zaten mevcut. Lütfen farklı bir isim giriniz.");
            }


            var newProject = new Project
            {
                ProjectName = projectDto.ProjectName,
                StartDate = projectDto.StartDate,
                EndDate = projectDto.EndDate,
                ProjectStatusId = projectDto.ProjectStatusId,
                ProjectDescription = projectDto.ProjectDescription,
                PriorityId = CalculatePriorityId(projectDto.EndDate)
            };

       

        await _projectRepository.AddAsync(newProject);

           
            await _projectRepository.SaveChangesAsync();



            return newProject;

        }
        
        public async Task<bool> DeleteProjectAsync(int projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
                return false;

            _projectRepository.Delete(project); 

            await _projectRepository.SaveChangesAsync();

            return true;
        }

        
        public async Task<Project> UpdateProjectAsync(int id, ProjectUpdateDto projectDto)
        {
            var existingProject = await _projectRepository.GetByIdAsync(id);

            if (existingProject == null)
            {
                throw new KeyNotFoundException("Güncellenmek istenen proje bulunamadı.");
            }

            if (projectDto.ProjectStatusId <= 0)
            {
                throw new ArgumentException("Proje durum ID'si (ProjectStatusId) zorunludur ve geçerli bir değer olmalıdır.");
            }

            if (projectDto.StartDate > projectDto.EndDate)
            {
                throw new ArgumentException("Proje başlangıç tarihi (StartDate), bitiş tarihinden (EndDate) büyük olamaz.");
            }

            var allProjects = await _projectRepository.GetAllAsync();
            var isProjectNameExists = allProjects.Any(p =>
                p.Id != id &&
                p.ProjectName.Equals(projectDto.ProjectName, StringComparison.OrdinalIgnoreCase));

            if (isProjectNameExists)
            {
                throw new ArgumentException($"'{projectDto.ProjectName}' adında başka bir proje zaten mevcut.");
            }

            existingProject.ProjectName = projectDto.ProjectName;
            existingProject.StartDate = projectDto.StartDate;
            existingProject.EndDate = projectDto.EndDate;
            existingProject.ProjectStatusId = projectDto.ProjectStatusId;
            existingProject.ProjectDescription = projectDto.ProjectDescription;
            existingProject.PriorityId = CalculatePriorityId(projectDto.EndDate);
            existingProject.UpdatedAt = DateTime.Now;
            _projectRepository.Update(existingProject);
            await _projectRepository.SaveChangesAsync();

       
            return existingProject;
        }
     }
 }
 
