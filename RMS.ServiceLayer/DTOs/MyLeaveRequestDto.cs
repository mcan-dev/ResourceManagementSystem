using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.DTOs;

public class MyLeaveRequestDto
{
    public int Id { get; set; }

    public string LeaveTypeName { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int TotalDays { get; set; }

    public int StatusId { get; set; }

    public string StatusName { get; set; } = string.Empty;
}