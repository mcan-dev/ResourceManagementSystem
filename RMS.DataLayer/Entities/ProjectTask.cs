using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class ProjectTask
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public string TaskName { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? TaskStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();
}
