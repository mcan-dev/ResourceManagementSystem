using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.DTOs
{
    public class ManagerTaskListDto
    {
        public int TaskId { get; set; }
        public string? TaskName { get; set; }
        public string? ProjectName { get; set; }
        public string? TaskStatus { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public List<AssigneeDto> Assignees { get; set; } = new List<AssigneeDto>();
        public decimal TotalAssignedHours { get; set; }
        public decimal TotalCompletedHours { get; set; }
        public decimal TotalRemainingHours => TotalAssignedHours - TotalCompletedHours;
        public int ProgressPercentage => TotalAssignedHours > 0
            ? (int)Math.Round((decimal)TotalCompletedHours / TotalAssignedHours * 100, 0)
            : 0;
    }

    public class CreateTaskWithAssignmentsDto
    {
       
        public int ProjectId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string TaskStatus { get; set; } = string.Empty;

        public List<TaskAssignmentRequestDto> Assignments { get; set; } = new();
    }

    public class TaskAssignmentRequestDto
    {
        public int EmployeeId { get; set; }
        public decimal AssignedHours { get; set; }
    }

    public class UpdateTaskAssignmentDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int EmployeeId { get; set; }
        public decimal AssignedHours { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class AddAssignmentDto
    {
        public int TaskId { get; set; }
        public int EmployeeId { get; set; }
        public int AssignedHours { get; set; }
    }

    public class UpdateAssignmentHoursDto
    {
        public int AssignedHours { get; set; }
    }

}
