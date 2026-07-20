using RMS.DataLayer.Entities;
using RMS.DataLayer.RmsDb;
using RMS.RepositoryLayer.Interfaces;

namespace RMS.RepositoryLayer.Repositories;

internal class EmployeePriorityRepository
    : GenericRepository<EmployeePriority>, IEmployeePriorityRepository
{
    public EmployeePriorityRepository(RmsContext context)
        : base(context)
    {
    }
}