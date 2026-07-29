using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.DTOs
{
    public class EmployeeDashboardDto
    {
  
        public int UsedCapacityPercentage { get; set; } 
        public int ActiveTasksCount { get; set; }
        public int RemainingWorkHours { get; set; } 
        public int ActiveProjectsCount { get; set; }
        public int PendingLeaveRequestsCount { get; set; }
        public List<EmployeeDashboardTaskDto> UpcomingTasks { get; set; } = new();
    }

    public class EmployeeDashboardTaskDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public DateOnly? Deadline { get; set; }
        public decimal AssignedHours { get; set; }
        public decimal CompletedHours { get; set; }
    }

    public class EmployeeProjectCapacityDto
    {
        public string ProjectName { get; set; } = string.Empty;
        public int CapacityPercentage { get; set; } 
    }
}
