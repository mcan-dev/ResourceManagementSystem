using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class TaskProgress
{
    public int Id { get; set; }

    public int TaskAssignmentId { get; set; }

    public string Status { get; set; } = null!;

    public decimal TotalHours { get; set; }

    public decimal CompletedHours { get; set; }

    public decimal? LeftHours { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual TaskAssignment TaskAssignment { get; set; } = null!;
}
