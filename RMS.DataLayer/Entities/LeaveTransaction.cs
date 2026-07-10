using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class LeaveTransaction
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int LeaveTypeId { get; set; }

    public int? LeaveRequestId { get; set; }

    public int TransactionType { get; set; }

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public string? Description { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual LeaveRequest? LeaveRequest { get; set; }

    public virtual LeaveType LeaveType { get; set; } = null!;
}
