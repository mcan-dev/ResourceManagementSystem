using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class LeaveStatus
{
    public int Id { get; set; }

    public string LeaveStatusName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Leave> Leaves { get; set; } = new List<Leave>();
}
