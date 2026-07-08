using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class ProjectRole
{
    public int Id { get; set; }

    public string ProjectRoleName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
}
