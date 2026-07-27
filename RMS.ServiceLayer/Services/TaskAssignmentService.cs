using Microsoft.EntityFrameworkCore;
using RMS.DataLayer.Entities;
using RMS.DataLayer.RmsDb;
using RMS.RepositoryLayer.Interfaces;
using RMS.RepositoryLayer.Repositories;
using RMS.ServiceLayer.DTOs;
using RMS.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.Services
{ 
    public class TaskAssignmentService : ITaskAssignmentService
    {

        private readonly ITaskAssignmentRepository _taskAssignmentRepository;
        private readonly IProjectTaskRepository _projectTaskRepository;
        public TaskAssignmentService(ITaskAssignmentRepository taskAssignmentRepository, IProjectTaskRepository projectTaskRepository)
        {
            _taskAssignmentRepository = taskAssignmentRepository;
            _projectTaskRepository = projectTaskRepository;
        }

        public async Task<bool> CreateTaskWithAssignmentsAsync(CreateTaskWithAssignmentsDto dto)
        {
            
            var newTask = new ProjectTask
            {
                ProjectId = dto.ProjectId,
                TaskName = dto.TaskName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                TaskStatus = dto.TaskStatus
            };  

           
            await _projectTaskRepository.AddAsync(newTask);
            await _projectTaskRepository.SaveChangesAsync();
 
            if (dto.Assignments != null && dto.Assignments.Any())
            {
                foreach (var assignment in dto.Assignments)
                {
                    var newAssignment = new TaskAssignment
                    {
                    
                        TaskId = newTask.Id,
                        EmployeeId = assignment.EmployeeId,
                        AssignedHours = assignment.AssignedHours
                    };

                    await _taskAssignmentRepository.AddAsync(newAssignment);
                }

              
                await _taskAssignmentRepository.SaveChangesAsync();
            }

            return true;
        }

        public async Task<List<EmployeeTaskListDto>> GetTasksByEmployeeIdAsync(int employeeId)
        {
            var assignments = await _taskAssignmentRepository
                .GetAllWithDetailsAsync(ta => ta.EmployeeId == employeeId);

            var employeeTasks = assignments.Select(ta => new EmployeeTaskListDto
            {
                TaskId = ta.TaskId,
                TaskName = ta.Task?.TaskName ?? "Bilinmeyen Görev",
                ProjectName = ta.Task?.Project?.ProjectName ?? "Bilinmeyen Proje",
                TaskStatus = ta.Task?.TaskStatus ?? "Belirtilmedi",
                StartDate = ta.Task?.StartDate,
                EndDate = ta.Task?.EndDate,
                AssignedHours = ta.AssignedHours ?? 0,
                CompletedHours = ta.CompletedHours

            }).ToList();

            return employeeTasks;
        }



        public async Task<List<ManagerTaskListDto>> GetAllManagerTasksAsync()
        {
            var allTasks = await _projectTaskRepository.GetAllWithDetailsAsync();

            var managerTasks = allTasks.Select(task => new ManagerTaskListDto
            {
                TaskId = task.Id,
                TaskName = task.TaskName ?? "Bilinmeyen Görev",
                ProjectName = task.Project?.ProjectName ?? "Bilinmeyen Proje",
                TaskStatus = task.TaskStatus ?? "Belirtilmedi",
                StartDate = task.StartDate,
                EndDate = task.EndDate,

                Assignees = task.TaskAssignments?.Select(ta => new AssigneeDto
                {
                    Id = ta.Id,
                    EmployeeName = ta.Employee?.Name ?? "Bilinmeyen Personel",
                    AssignedHours = ta.AssignedHours ?? 0,
                    CompletedHours = ta.CompletedHours
                }).ToList() ?? new List<AssigneeDto>(),

                TotalAssignedHours = task.TaskAssignments?.Sum(ta => ta.AssignedHours ?? 0) ?? 0,
                TotalCompletedHours = task.TaskAssignments?.Sum(ta => ta.CompletedHours) ?? 0

            }).ToList();

            return managerTasks;
        }
        public async Task<bool> DeleteTaskAsync(int taskId)
        {
           
            var taskToDelete = await _projectTaskRepository.GetByIdAsync(taskId);

            if (taskToDelete == null)
            {
                return false;
            }

            _projectTaskRepository.Delete(taskToDelete);
            int result = await _projectTaskRepository.SaveChangesAsync();
   
            return result > 0;
        }

        public async Task AddSingleAssignmentAsync(AddAssignmentDto dto)
        {
            var newAssignment = new TaskAssignment
            {
                TaskId = dto.TaskId,
                EmployeeId = dto.EmployeeId,
                AssignedHours = dto.AssignedHours
            };

            await _taskAssignmentRepository.AddAsync(newAssignment);

            await _taskAssignmentRepository.SaveChangesAsync();
        }

        public async Task UpdateAssignmentHoursAsync(int id, int newHours)
        {
         
            var assignment = await _taskAssignmentRepository.GetByIdAsync(id);
            if (assignment != null)
            {
                assignment.AssignedHours = newHours;
                _taskAssignmentRepository.Update(assignment);
            }

            /* 
            // --- İLERİSİ İÇİN YORUM SATIRI (TASK_PROGRESS) ---
            // Not: POST (Ekleme) işlemleri tam olarak oturtulduktan sonra bu blok açılacak.

            var progressList = await _taskProgressRepository.FindAsync(p => p.TaskAssignmentId == id); 
            var progress = progressList.FirstOrDefault();

            if (progress != null)
            {
                progress.TotalHours = newHours;
                progress.LeftHours = newHours - progress.CompletedHours;

                if(progress.LeftHours < 0) progress.LeftHours = 0; 

                if(progress.LeftHours == 0 && progress.CompletedHours > 0)
                    progress.Status = "Tamamlandı";
                else if (progress.LeftHours > 0 && progress.Status == "Tamamlandı")
                    progress.Status = "Devam Ediyor"; 

                _taskProgressRepository.Update(progress); 
            }
            */

            // 2. RAM'deki değişikliği veritabanına kaydet (Sadece TaskAssignment güncellenecek)
            await _taskAssignmentRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteSingleAssignmentAsync(int id)
        {
            var assignment = await _taskAssignmentRepository.GetByIdAsync(id);

      
            if (assignment == null)
            {
                return false;
            }

            _taskAssignmentRepository.Delete(assignment);

            await _taskAssignmentRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateCompletedHoursAsync(UpdateEmployeeProgressDto dto)
        {

            var assignments = await _taskAssignmentRepository
              .GetAllWithDetailsAsync(ta => ta.TaskId == dto.TaskId && ta.EmployeeId == dto.EmployeeId);

            var assignment = assignments.FirstOrDefault();

            if (assignment == null)
                return false;

            assignment.CompletedHours = dto.CompletedHours;

            assignment.UpdatedAt = DateTime.Now;

            _taskAssignmentRepository.Update(assignment);
            int result = await _taskAssignmentRepository.SaveChangesAsync();

            return result > 0;
        }

    }


}