using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class LeaveType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int DefaultDays { get; set; }

    public bool IsPaid { get; set; }

    public virtual ICollection<LeaveTransaction> LeaveTransactions { get; set; } = new List<LeaveTransaction>();
    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
    = new List<LeaveRequest>();
}
