using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class Team
{
    public int Id { get; set; }

    public string TeamName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
