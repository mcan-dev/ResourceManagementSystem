using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Gender { get; set; }

    public int? TeamId { get; set; }

    public int? TitleId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public string? UserRole { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<EmployeeCapacity> EmployeeCapacities { get; set; } = new List<EmployeeCapacity>();

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<LeaveTransaction> LeaveTransactions { get; set; } = new List<LeaveTransaction>();

    public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();

    public virtual ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();

    public virtual Team? Team { get; set; }

    public virtual Title? Title { get; set; }

    public virtual ICollection<WorkCalendar> WorkCalendars { get; set; } = new List<WorkCalendar>();
}
