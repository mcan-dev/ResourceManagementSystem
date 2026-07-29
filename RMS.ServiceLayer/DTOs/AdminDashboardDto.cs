using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.DTOs
{
    public class AdminDashboardDto
    {
        public int TotalCapacityHours { get; set; }
        public int UsedCapacityHours { get; set; }
        public int AvailableCapacityHours { get; set; }
        public int UsagePercentage { get; set; }
        public int UpcomingProjectsCount { get; set; }
        public int PendingLeaveRequestsCount { get; set; }
        public List<TeamCapacityDto> TeamCapacities { get; set; } = new(); 
        public List<DashboardTaskDto> UpcomingTasks { get; set; } = new();
        public List<CriticalEmployeeDto> CriticalEmployees { get; set; } = new();
    }

    public class DashboardTaskDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public string Assignees { get; set; } = string.Empty; 
        public string StatusName { get; set; } = string.Empty;
        public DateOnly? Deadline { get; set; }
    }

    public class CriticalEmployeeDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeInitials { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string TitleName { get; set; } = string.Empty;
        public int UsedCapacityPercentage { get; set; }
    }
}
