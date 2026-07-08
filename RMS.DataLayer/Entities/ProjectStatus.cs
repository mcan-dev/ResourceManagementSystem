using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class ProjectStatus
{
    public int Id { get; set; }

    public string ProjectStatus1 { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
