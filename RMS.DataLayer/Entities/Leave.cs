using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class Leave
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int? TotalDays { get; set; }

    public string? LeaveType { get; set; }

    public int? LeaveStatusId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual LeaveStatus? LeaveStatus { get; set; }
}
