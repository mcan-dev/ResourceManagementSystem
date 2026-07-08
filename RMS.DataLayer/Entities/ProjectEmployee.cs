using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class ProjectEmployee
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public int EmployeeId { get; set; }

    public int? ProjectRoleId { get; set; }

    public int? EmployeePriorityId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual EmployeePriority? EmployeePriority { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual ProjectRole? ProjectRole { get; set; }
}
