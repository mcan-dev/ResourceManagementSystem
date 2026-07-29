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
    public class DashboardService : IDashboardService
    {
        private readonly IEmployeeCapacityService _employeeCapacityService;
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ITaskAssignmentRepository _taskAssignmentRepository;

        public DashboardService(
            IEmployeeCapacityService employeeCapacityService,
            IProjectTaskRepository projectTaskRepository,
            IProjectRepository projectRepository,
            ILeaveRequestRepository leaveRequestRepository, 
            ITaskAssignmentRepository taskAssignmentRepository)
        {
            _employeeCapacityService = employeeCapacityService;
            _projectTaskRepository = projectTaskRepository;
            _projectRepository = projectRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _taskAssignmentRepository = taskAssignmentRepository;
        }

        public async Task<AdminDashboardDto> GetAdminDashboardDataAsync()
        {
            var dashboardData = new AdminDashboardDto();

            var workloads = await _employeeCapacityService.GetEmployeeWorkloadsAsync();

            dashboardData.TotalCapacityHours = workloads.Count * 80;

            dashboardData.AvailableCapacityHours = workloads.Sum(w => w.Capacity);

            dashboardData.UsedCapacityHours = dashboardData.TotalCapacityHours - dashboardData.AvailableCapacityHours;

            dashboardData.UsagePercentage = dashboardData.TotalCapacityHours > 0
                ? (int)Math.Round(((double)dashboardData.UsedCapacityHours / dashboardData.TotalCapacityHours) * 100)
                : 0;

   
            dashboardData.TeamCapacities = (await _employeeCapacityService.GetTeamCapacitiesAsync()).ToList();

            
            dashboardData.CriticalEmployees = workloads
                .Where(w => w.Capacity <= 16) 
                .Select(w => new CriticalEmployeeDto
                {
                    EmployeeId = w.EmployeeId,
                    EmployeeName = w.EmployeeName,
                    TitleName = w.TitleName,
                    EmployeeInitials = string.Join("", w.EmployeeName.Split(' ').Select(n => n[0])).ToUpper(),

                    UsedCapacityPercentage = (int)Math.Round(((double)(80 - w.Capacity) / 80) * 100)
                })
                .OrderByDescending(c => c.UsedCapacityPercentage)
                .ToList();

            var bugun = DateOnly.FromDateTime(DateTime.Now);

            var allProjects = await _projectRepository.GetAllAsync();
            dashboardData.UpcomingProjectsCount = allProjects.Count(p =>
                p.ProjectStatusId != 6 && p.ProjectStatusId != 7 && 
                p.EndDate.HasValue && (p.EndDate.Value.DayNumber - bugun.DayNumber) <= 14);

            var allLeaves = await _leaveRequestRepository.GetAllAsync();
            dashboardData.PendingLeaveRequestsCount = allLeaves.Count(l => l.LeaveStatusId == 1);

            
            var allTasks = await _projectTaskRepository.GetAllWithDetailsAsync(); 

            dashboardData.UpcomingTasks = allTasks
                .Where(t => t.TaskStatus != "Tamamlandı" && t.EndDate.HasValue)
                .OrderBy(t => t.EndDate)
                .Take(5)
                .Select(t => new DashboardTaskDto
                {
                    TaskId = t.Id,
                    TaskName = t.TaskName,
                    ProjectName = t.Project?.ProjectName ?? "Bilinmeyen Proje",
                    StatusName = t.TaskStatus ?? "Belirtilmedi",
                    Deadline = t.EndDate,
                    Assignees = t.TaskAssignments != null && t.TaskAssignments.Any()
                        ? string.Join(", ", t.TaskAssignments.Select(a => a.Employee?.Name))
                        : "Atanmadı"
                })
                .ToList();

            return dashboardData;
        }

        public async Task<EmployeeDashboardDto> GetEmployeeDashboardDataAsync(int employeeId)
        {
            var dashboardData = new EmployeeDashboardDto();

            var employeeAssignments = await _taskAssignmentRepository
                .GetAllWithDetailsAsync(ta => ta.EmployeeId == employeeId);

            var activeAssignments = employeeAssignments
                .Where(ta => ta.Task != null && ta.Task.TaskStatus != "Tamamlandı")
                .ToList();

            int totalAssignedHours = (int)employeeAssignments.Sum(a => a.AssignedHours ?? 0);

            dashboardData.UsedCapacityPercentage = (int)Math.Round(((double)totalAssignedHours / 80) * 100);
            dashboardData.ActiveTasksCount = activeAssignments.Count;
            dashboardData.RemainingWorkHours = (int)activeAssignments.Sum(a => (a.AssignedHours ?? 0) - a.CompletedHours);
            dashboardData.ActiveProjectsCount = activeAssignments.Select(a => a.Task?.ProjectId).Distinct().Count();

            var leaves = await _leaveRequestRepository.FindAsync(l => l.EmployeeId == employeeId && l.LeaveStatusId == 1);
            dashboardData.PendingLeaveRequestsCount = leaves.Count();

            dashboardData.UpcomingTasks = activeAssignments
                .Where(a => a.Task?.EndDate.HasValue == true)
                .OrderBy(a => a.Task?.EndDate)
                .Take(5)
                .Select(a => new EmployeeDashboardTaskDto
                {
                    TaskId = a.TaskId,
                    TaskName = a.Task?.TaskName ?? "Bilinmeyen Görev",
                    ProjectName = a.Task?.Project?.ProjectName ?? "Bilinmeyen Proje",
                    StatusName = a.Task?.TaskStatus ?? "Belirtilmedi",
                    Deadline = a.Task?.EndDate,
                    AssignedHours = a.AssignedHours ?? 0,
                    CompletedHours = a.CompletedHours
                })
                .ToList();

            return dashboardData;
        }
    }
}

