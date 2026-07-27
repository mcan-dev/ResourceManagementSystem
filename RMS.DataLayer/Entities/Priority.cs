using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class Priority
{
    public int Id { get; set; }

    public string PriorityName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
