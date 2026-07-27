using RMS.DataLayer.Entities;
using RMS.DataLayer.RmsDb;
using RMS.RepositoryLayer.Interfaces;

namespace RMS.RepositoryLayer.Repositories;

public class EmployeePriorityRepository : GenericRepository<EmployeePriority>, IEmployeePriorityRepository
{
    public EmployeePriorityRepository(RmsContext context) : base(context)
    {

    }
}