using System;
using System.Collections.Generic;

namespace RMS.DataLayer.Entities;

public partial class WorkCalendar
{
    public int Id { get; set; }

    public DateOnly CalendarDate { get; set; }

    public bool IsWorkingDay { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public decimal? WorkingHours { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Employee? Employee { get; set; }
}
