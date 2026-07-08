using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class EmployeePriority
{
    public int Id { get; set; }

    public string PriorityName { get; set; } = null!;

    public int PriorityLevel { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
}
