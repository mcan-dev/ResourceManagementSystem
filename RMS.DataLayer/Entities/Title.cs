using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class Title
{
    public int Id { get; set; }

    public string TitleName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
