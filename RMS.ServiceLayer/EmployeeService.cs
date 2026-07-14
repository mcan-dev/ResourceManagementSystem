using RMS.DataLayer.Entities;
using RMS.RepositoryLayer.Interfaces;
using RMS.ServiceLayer.DTOs;

namespace RMS.ServiceLayer;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IReadOnlyList<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellationToken = default)
    {
        var employees = await _employeeRepository.GetAllEmployeesWithDetailsAsync();
        return employees.Select(MapToDto).ToList();
    }

    public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetEmployeeWithDetailsByIdAsync(id);
        return employee is null ? null : MapToDto(employee);
    }

    public async Task<EmployeeDto?> GetEmployeeByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetEmployeeByEmailAsync(email);
        return employee is null ? null : MapToDto(employee);
    }

    public async Task<IReadOnlyList<EmployeeDto>> GetEmployeesByTeamAsync(int teamId, CancellationToken cancellationToken = default)
    {
        var employees = await _employeeRepository.GetEmployeesByTeamIdAsync(teamId);
        return employees.Select(MapToDto).ToList();
    }

    public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeRequest request, CancellationToken cancellationToken = default)
    {
        ValidateRequest(request);

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByEmailAsync(request.Email);
            if (existingEmployee is not null)
            {
                throw new InvalidOperationException($"An employee with email '{request.Email}' already exists.");
            }
        }

        if (!string.IsNullOrWhiteSpace(request.Username))
        {
            var existingEmployee = await _employeeRepository.FindAsync(e => e.Username == request.Username);
            if (existingEmployee.Any())
            {
                throw new InvalidOperationException($"An employee with username '{request.Username}' already exists.");
            }
        }

        var employee = new Employee
        {
            Name = request.Name.Trim(),
            Surname = request.Surname.Trim(),
            Gender = request.Gender,
            TeamId = request.TeamId,
            TitleId = request.TitleId,
            Username = request.Username?.Trim(),
            Email = request.Email?.Trim(),
            PasswordHash = request.Password,
            UserRole = request.UserRole,
            Status = string.IsNullOrWhiteSpace(request.Status) ? "active" : request.Status.Trim(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _employeeRepository.AddAsync(employee);
        await _employeeRepository.SaveChangesAsync(cancellationToken);
        return MapToDto(employee);
    }

    public async Task<EmployeeDto?> UpdateEmployeeAsync(int id, UpdateEmployeeRequest request, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetEmployeeWithDetailsByIdAsync(id);
        if (employee is null)
        {
            return null;
        }

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByEmailAsync(request.Email);
            if (existingEmployee is not null && existingEmployee.Id != id)
            {
                throw new InvalidOperationException($"An employee with email '{request.Email}' already exists.");
            }
        }

        if (!string.IsNullOrWhiteSpace(request.Username))
        {
            var existingEmployee = await _employeeRepository.FindAsync(e => e.Username == request.Username);
            if (existingEmployee.Any(e => e.Id != id))
            {
                throw new InvalidOperationException($"An employee with username '{request.Username}' already exists.");
            }
        }

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            employee.Name = request.Name.Trim();
        }

        if (!string.IsNullOrWhiteSpace(request.Surname))
        {
            employee.Surname = request.Surname.Trim();
        }

        if (request.Gender is not null)
        {
            employee.Gender = request.Gender;
        }

        if (request.TeamId.HasValue)
        {
            employee.TeamId = request.TeamId;
        }

        if (request.TitleId.HasValue)
        {
            employee.TitleId = request.TitleId;
        }

        if (request.Username is not null)
        {
            employee.Username = request.Username.Trim();
        }

        if (request.Email is not null)
        {
            employee.Email = request.Email.Trim();
        }

        if (request.Password is not null)
        {
            employee.PasswordHash = request.Password;
        }

        if (request.UserRole is not null)
        {
            employee.UserRole = request.UserRole;
        }

        if (request.Status is not null)
        {
            employee.Status = request.Status.Trim();
        }

        employee.UpdatedAt = DateTime.UtcNow;
        _employeeRepository.Update(employee);
        await _employeeRepository.SaveChangesAsync(cancellationToken);
        return MapToDto(employee);
    }

    public async Task<bool> DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee is null)
        {
            return false;
        }

        _employeeRepository.Delete(employee);
        await _employeeRepository.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static void ValidateRequest(CreateEmployeeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Employee name is required.", nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Surname))
        {
            throw new ArgumentException("Employee surname is required.", nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Status))
        {
            throw new ArgumentException("Employee status is required.", nameof(request));
        }
        var validStatuses = new[] { "active", "passive", "on_leave" };

        if (!validStatuses.Contains(request.Status))
        {
            throw new ArgumentException("Status must be active, passive or on_leave.", nameof(request));
        }
    }

    private static EmployeeDto MapToDto(Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Surname = employee.Surname,
            Gender = employee.Gender,
            TeamId = employee.TeamId,
            TitleId = employee.TitleId,
            TeamName = employee.Team?.TeamName,
            TitleName = employee.Title?.TitleName,
            Username = employee.Username,
            Email = employee.Email,
            UserRole = employee.UserRole,
            Status = employee.Status,
            CreatedAt = employee.CreatedAt,
            UpdatedAt = employee.UpdatedAt
        };
    }
}
