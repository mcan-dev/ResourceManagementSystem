using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class EmployeeCapacity
{
    public int Id { get; set; }

    public int Capacity { get; set; }

    public int ProjectId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
