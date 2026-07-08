using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class TaskAssignment
{
    public int Id { get; set; }

    public int TaskId { get; set; }

    public int EmployeeId { get; set; }

    public decimal? AssignedHours { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual ProjectTask Task { get; set; } = null!;
}
