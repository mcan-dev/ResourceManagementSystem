using RMS.DataLayer.Entities;
using RMS.RepositoryLayer.Interfaces;
using RMS.ServiceLayer.DTOs;

namespace RMS.ServiceLayer
{
    public class SettingsService : ISettingsService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public SettingsService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> ChangeEmailAsync(ChangeEmailDto dto)
        {
            // Mailler aynı mı?
            if (dto.NewEmail != dto.ConfirmEmail)
                return false;

            // Aynı mail başka kullanıcıda var mı?
            var existingEmployee = await _employeeRepository.GetEmployeeByEmailAsync(dto.NewEmail);

            if (existingEmployee != null && existingEmployee.Id != dto.EmployeeId)
                return false;

            return await _employeeRepository.UpdateEmailAsync(dto.EmployeeId, dto.NewEmail);
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(dto.EmployeeId);

            if (employee == null)
                throw new Exception("Employee not found");

            if (employee.PasswordHash != dto.CurrentPassword)
                throw new Exception("Current password is incorrect");

            if (dto.NewPassword != dto.ConfirmPassword)
                throw new Exception("Passwords do not match");

            return await _employeeRepository.UpdatePasswordAsync(dto.EmployeeId, dto.NewPassword);
        }
    }
}