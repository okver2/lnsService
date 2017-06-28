using System.Collections.Generic;
using System.Threading.Tasks;

namespace lns.services.Employees
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
    }
}
