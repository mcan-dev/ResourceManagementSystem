using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.DTOs;

    public class TeamCapacityDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public int EmployeeCount { get; set; }
        public int AverageCapacity { get; set; }
    }

    public class EmployeeWorkloadDto
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = string.Empty;

    public string TitleName { get; set; } = string.Empty;

    public int TeamId { get; set; }

    public string TeamName { get; set; } = string.Empty;

    public int Capacity { get; set; }
}

    public class EmployeeCapacityDetailDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public int Capacity { get; set; }
        
    }
public class EmployeeCapacityDto
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = string.Empty;

    public int Capacity { get; set; }
}

public class CreateEmployeeCapacityRequest
{
    public int EmployeeId { get; set; }

    public int Capacity { get; set; }
}

public class UpdateEmployeeCapacityRequest
{
    public int Capacity { get; set; }
}

public class EmployeeCapacitySummaryDto
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = string.Empty;

    public int TotalCapacity { get; set; }

    public int RemainingCapacity { get; set; }

}


