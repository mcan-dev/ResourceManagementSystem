using RMS.RepositoryLayer.Interfaces;
using RMS.ServiceLayer.DTOs;
using RMS.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeListDto>> GetAllEmployeesForDropdownAsync()
        {
         
            var employees = await _employeeRepository.GetAllAsync();

            return employees.Select(e => new EmployeeListDto
            {
                Id = e.Id,
                Name = e.Name, 
                Surname = e.Surname
            }).ToList();
        }
    }
}
