using RMS.DataLayer.Entities;
using RMS.RepositoryLayer.Interfaces;
using RMS.ServiceLayer.DTOs;
using RMS.ServiceLayer.Interfaces;
using System.Linq;

namespace RMS.ServiceLayer.Services;

public class EmployeeCapacityService : IEmployeeCapacityService
{
    private readonly IEmployeeCapacityRepository _employeeCapacityRepository;
    private readonly ITaskAssignmentRepository _taskAssignmentRepository;
   
    public EmployeeCapacityService(IEmployeeCapacityRepository employeeCapacityRepository, ITaskAssignmentRepository taskAssignmentRepository)
    {
        _employeeCapacityRepository = employeeCapacityRepository;
        _taskAssignmentRepository = taskAssignmentRepository;
    }

    private async Task<int> GetEmployeeAssignedHoursAsync(int employeeId)
    {

        var assignments = await _taskAssignmentRepository.FindAsync(t => t.EmployeeId == employeeId);

        return (int)(assignments?.Sum(t => t.AssignedHours) ?? 0);
    }

    public async Task<IReadOnlyList<TeamCapacityDto>> GetTeamCapacitiesAsync()
    {
        var capacities = await _employeeCapacityRepository
            .GetEmployeeCapacitiesWithDetailsAsync();

        var groupedCapacities = capacities
            .GroupBy(ec => ec.Employee.TeamId);

        var result = new List<TeamCapacityDto>();

        foreach (var group in groupedCapacities)
        {

            var teamRemainingCapacities = new List<int>();

            foreach (var capacity in group)
            {
                int assignedHours = await GetEmployeeAssignedHoursAsync(capacity.EmployeeId);
                int remaining = 80 - assignedHours;
                teamRemainingCapacities.Add(remaining < 0 ? 0 : remaining); 
            }

            result.Add(new TeamCapacityDto
            {
                TeamId = group.Key,
                TeamName = group.First().Employee.Team?.TeamName ?? string.Empty,
                EmployeeCount = group.Select(x => x.EmployeeId).Distinct().Count(),
                AverageCapacity = teamRemainingCapacities.Any() ? (int)Math.Round(teamRemainingCapacities.Average()) : 0
            });
        }

        return result;
    }

    public async Task<IReadOnlyList<EmployeeWorkloadDto>> GetEmployeeWorkloadsAsync()
    {
        var capacities = await _employeeCapacityRepository
            .GetEmployeeCapacitiesWithDetailsAsync();

        var result = new List<EmployeeWorkloadDto>();

        foreach (var capacity in capacities)
        {
            int assignedHours = await GetEmployeeAssignedHoursAsync(capacity.EmployeeId);
            int remainingCapacity = 80 - assignedHours;

            result.Add(new EmployeeWorkloadDto
            {
                EmployeeId = capacity.EmployeeId,
                EmployeeName = $"{capacity.Employee.Name} {capacity.Employee.Surname}",
                TeamId = capacity.Employee.TeamId,
                TeamName = capacity.Employee.Team?.TeamName ?? "",
                TitleName = capacity.Employee.Title?.TitleName ?? "",
                Capacity = remainingCapacity < 0 ? 0 : remainingCapacity 
            });
        }

        return result.OrderByDescending(x => x.Capacity).ToList();
    }

    public async Task<IReadOnlyList<EmployeeCapacityDetailDto>> GetEmployeeCapacityDetailsAsync(int employeeId)
    {
        var capacities = await _employeeCapacityRepository
            .GetEmployeeCapacitiesWithDetailsAsync();

        var targetCapacities = capacities.Where(x => x.EmployeeId == employeeId).ToList();
        var result = new List<EmployeeCapacityDetailDto>();

        foreach (var x in targetCapacities)
        {
            int assignedHours = await GetEmployeeAssignedHoursAsync(x.EmployeeId);
            int remainingCapacity = 80 - assignedHours;

            result.Add(new EmployeeCapacityDetailDto
            {
                EmployeeId = x.EmployeeId,
                EmployeeName = $"{x.Employee.Name} {x.Employee.Surname}",
                Capacity = remainingCapacity < 0 ? 0 : remainingCapacity
            });
        }

        return result.OrderByDescending(x => x.Capacity).ToList();
    }

    public async Task<IReadOnlyList<EmployeeCapacityDto>> GetCapacitiesAsync(int? employeeId = null)
    {
        var capacities = await _employeeCapacityRepository
            .GetEmployeeCapacitiesWithDetailsAsync();

        if (employeeId.HasValue)
        {
            capacities = capacities
                .Where(x => x.EmployeeId == employeeId.Value)
                .ToList();
        }

        var result = new List<EmployeeCapacityDto>();

        foreach (var x in capacities)
        {
            int assignedHours = await GetEmployeeAssignedHoursAsync(x.EmployeeId);
            int remainingCapacity = 80 - assignedHours;

            result.Add(new EmployeeCapacityDto
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                EmployeeName = $"{x.Employee.Name} {x.Employee.Surname}",
                Capacity = remainingCapacity < 0 ? 0 : remainingCapacity
            });
        }

        return result;
    }

    public async Task<EmployeeCapacityDto> CreateCapacityAsync(CreateEmployeeCapacityRequest request)
    {
        if (request.Capacity < 0 || request.Capacity > 80)
        {
            throw new InvalidOperationException("Kapasite 0 ile 80 saat arasında olmalıdır.");
        }

        var exists = await _employeeCapacityRepository.ExistsAsync(request.EmployeeId);
        if (exists)
        {
            throw new InvalidOperationException("Bu personelin zaten bir kapasite kaydı bulunmaktadır.");
        }

        var employeeCapacity = new EmployeeCapacity
        {
            EmployeeId = request.EmployeeId,
            Capacity = request.Capacity, 
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _employeeCapacityRepository.AddAsync(employeeCapacity);
        await _employeeCapacityRepository.SaveChangesAsync();

        return new EmployeeCapacityDto
        {
            Id = employeeCapacity.Id,
            EmployeeId = employeeCapacity.EmployeeId,
            Capacity = employeeCapacity.Capacity
        };
    }

    public async Task<EmployeeCapacityDto?> UpdateCapacityAsync(int id, UpdateEmployeeCapacityRequest request)
    {
        if (request.Capacity < 0 || request.Capacity > 80)
        {
            throw new InvalidOperationException("Kapasite 0 ile 80 saat arasında olmalıdır.");
        }

        var employeeCapacity = await _employeeCapacityRepository.GetByIdAsync(id);

        if (employeeCapacity is null)
        {
            return null;
        }

        employeeCapacity.Capacity = request.Capacity;
        employeeCapacity.UpdatedAt = DateTime.UtcNow;

        _employeeCapacityRepository.Update(employeeCapacity);
        await _employeeCapacityRepository.SaveChangesAsync();

        return new EmployeeCapacityDto
        {
            Id = employeeCapacity.Id,
            EmployeeId = employeeCapacity.EmployeeId,
            Capacity = employeeCapacity.Capacity
        };
    }

    public async Task<EmployeeCapacitySummaryDto?> GetEmployeeCapacitySummaryAsync(int employeeId)
    {
        var capacities = await _employeeCapacityRepository
            .GetEmployeeCapacitiesByEmployeeAsync(employeeId);

        if (!capacities.Any())
        {
            return null;
        }

        var employee = capacities.First().Employee;

        int assignedHours = await GetEmployeeAssignedHoursAsync(employeeId);
        int remainingCapacity = 80 - assignedHours;

        return new EmployeeCapacitySummaryDto
        {
            EmployeeId = employee.Id,
            EmployeeName = $"{employee.Name} {employee.Surname}",
            TotalCapacity = 80, 
            RemainingCapacity = remainingCapacity < 0 ? 0 : remainingCapacity
        };
    }
}