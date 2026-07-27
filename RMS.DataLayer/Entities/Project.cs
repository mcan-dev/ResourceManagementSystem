using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class Project
{
    public int Id { get; set; }

    public string ProjectName { get; set; } = null!;

    public int ProjectStatusId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? PriorityId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? ProjectDescription { get; set; }

    public virtual ICollection<EmployeeCapacity> EmployeeCapacities { get; set; } = new List<EmployeeCapacity>();

    public virtual Priority? Priority { get; set; }

    public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();

    public virtual ProjectStatus ProjectStatus { get; set; } = null!;

    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
}
