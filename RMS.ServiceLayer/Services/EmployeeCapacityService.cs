using RMS.DataLayer.Entities;
using RMS.RepositoryLayer.Interfaces;
using RMS.ServiceLayer.DTOs;
using RMS.ServiceLayer.Interfaces;

namespace RMS.ServiceLayer.Services;

public class EmployeeCapacityService : IEmployeeCapacityService
{
    private readonly IEmployeeCapacityRepository _employeeCapacityRepository;

    public EmployeeCapacityService(IEmployeeCapacityRepository employeeCapacityRepository)
    {
        _employeeCapacityRepository = employeeCapacityRepository;
    }
    private async Task<int> GetEmployeeTotalCapacityAsync(int employeeId)
    {
        var employeeCapacities = await _employeeCapacityRepository
            .GetEmployeeCapacitiesByEmployeeAsync(employeeId);

        return employeeCapacities.Sum(x => x.Capacity);
    }
    public async Task<IReadOnlyList<TeamCapacityDto>> GetTeamCapacitiesAsync()
    {
       var capacities = await _employeeCapacityRepository
       .GetEmployeeCapacitiesWithDetailsAsync();

       var groupedCapacities = capacities
       .GroupBy(ec => ec.Employee.TeamId);

        var result = groupedCapacities
    .Select(group => new TeamCapacityDto
    {
        TeamId = group.Key,
        TeamName = group.First().Employee.Team?.TeamName ?? string.Empty,

        EmployeeCount = group
            .Select(x => x.EmployeeId)
            .Distinct()
            .Count(),

        AverageCapacity = (int)Math.Round(group.Average(x => x.Capacity))
    })
    .ToList();

        return result;
    }

    public async Task<IReadOnlyList<EmployeeWorkloadDto>> GetEmployeeWorkloadsAsync()
    {
        var capacities = await _employeeCapacityRepository
            .GetEmployeeCapacitiesWithDetailsAsync();
        var first = capacities.First();

        var result = capacities
     .Select(capacity => new EmployeeWorkloadDto
     {
         EmployeeId = capacity.EmployeeId,
         EmployeeName = $"{capacity.Employee.Name} {capacity.Employee.Surname}",
         TeamId = capacity.Employee.TeamId,
         TeamName = capacity.Employee.Team?.TeamName ?? "",
         TitleName = capacity.Employee.Title?.TitleName ?? "",
         Capacity = capacity.Capacity
     })
     .OrderByDescending(x => x.Capacity)
     .ToList();

        return result;
    }

    public async Task<IReadOnlyList<EmployeeCapacityDetailDto>> GetEmployeeCapacityDetailsAsync(int employeeId)
    {
        var capacities = await _employeeCapacityRepository
            .GetEmployeeCapacitiesWithDetailsAsync();

        var result = capacities
            .Where(x => x.EmployeeId == employeeId)
            .Select(x => new EmployeeCapacityDetailDto
            {
                EmployeeId = x.EmployeeId,
                EmployeeName = $"{x.Employee.Name} {x.Employee.Surname}",
                ProjectName = x.Project.ProjectName,
                Capacity = x.Capacity
            })
            .OrderByDescending(x => x.Capacity)
            .ToList();

        return result;
    }
    public async Task<IReadOnlyList<EmployeeCapacityDto>> GetCapacitiesAsync(
    int? projectId = null,
    int? employeeId = null)
    {
        var capacities = await _employeeCapacityRepository
            .GetEmployeeCapacitiesWithDetailsAsync();

        if (projectId.HasValue)
        {
            capacities = capacities
                .Where(x => x.ProjectId == projectId.Value)
                .ToList();
        }

        if (employeeId.HasValue)
        {
            capacities = capacities
                .Where(x => x.EmployeeId == employeeId.Value)
                .ToList();
        }

        return capacities
            .Select(x => new EmployeeCapacityDto
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                EmployeeName = $"{x.Employee.Name} {x.Employee.Surname}",
                ProjectId = x.ProjectId,
                ProjectName = x.Project.ProjectName,
                Capacity = x.Capacity
            })
            .ToList();
    }

    public async Task<EmployeeCapacityDto> CreateCapacityAsync(
    CreateEmployeeCapacityRequest request)
    {
        if (await _employeeCapacityRepository.ExistsAsync(
        request.EmployeeId,
        request.ProjectId))
        {
            throw new InvalidOperationException(
                "Bu personel bu projeye zaten atanmış.");
        }
        if (request.Capacity < 0)
        {
            throw new InvalidOperationException("Kapasite 0'dan küçük olamaz.");
        }
        var totalCapacity = await GetEmployeeTotalCapacityAsync(request.EmployeeId);

        var employeeCapacities = await _employeeCapacityRepository
    .GetEmployeeCapacitiesByEmployeeAsync(request.EmployeeId);

        if (totalCapacity + request.Capacity > 100)
        {
            throw new InvalidOperationException("Personelin toplam kapasitesi %100'ü aşamaz.");
        }
        if (await _employeeCapacityRepository.ExistsAsync(
        request.EmployeeId,
        request.ProjectId))
        {
            throw new InvalidOperationException(
                "Bu personel bu projeye zaten atanmış.");
        }

        var employeeCapacity = new EmployeeCapacity
        {
            EmployeeId = request.EmployeeId,
            ProjectId = request.ProjectId,
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
            ProjectId = employeeCapacity.ProjectId,
            Capacity = employeeCapacity.Capacity
        };
    }

    public async Task<EmployeeCapacityDto?> UpdateCapacityAsync(
    int id,
    UpdateEmployeeCapacityRequest request)
    {
        if (request.Capacity < 0)
        {
            throw new InvalidOperationException("Kapasite 0'dan küçük olamaz.");
        }

        var employeeCapacity = await _employeeCapacityRepository.GetByIdAsync(id);

        if (employeeCapacity is null)
        {
            return null;
        }

        var totalCapacity = await GetEmployeeTotalCapacityAsync(employeeCapacity.EmployeeId);

        var newTotalCapacity =
            totalCapacity
            - employeeCapacity.Capacity
            + request.Capacity;

        if (newTotalCapacity > 100)
        {
            throw new InvalidOperationException("Personelin toplam kapasitesi %100'ü aşamaz.");
        }

        employeeCapacity.Capacity = request.Capacity;
        employeeCapacity.UpdatedAt = DateTime.UtcNow;

        _employeeCapacityRepository.Update(employeeCapacity);

        await _employeeCapacityRepository.SaveChangesAsync();

        return new EmployeeCapacityDto
        {
            Id = employeeCapacity.Id,
            EmployeeId = employeeCapacity.EmployeeId,
            ProjectId = employeeCapacity.ProjectId,
            Capacity = employeeCapacity.Capacity
        };
    }

    public async Task<EmployeeCapacitySummaryDto?> GetEmployeeCapacitySummaryAsync(
    int employeeId)
    {
        var capacities = await _employeeCapacityRepository
            .GetEmployeeCapacitiesByEmployeeAsync(employeeId);

        if (!capacities.Any())
        {
            return null;
        }

        var employee = capacities.First().Employee;

        var totalCapacity = capacities.Sum(x => x.Capacity);

        return new EmployeeCapacitySummaryDto
        {
            EmployeeId = employee.Id,
            EmployeeName = $"{employee.Name} {employee.Surname}",
            TotalCapacity = totalCapacity,
            RemainingCapacity = 100 - totalCapacity,

            ProjectCapacities = capacities
                .Select(x => new ProjectCapacityDto
                {
                    ProjectName = x.Project.ProjectName,
                    Capacity = x.Capacity
                })
                .ToList()
        };
    }


}