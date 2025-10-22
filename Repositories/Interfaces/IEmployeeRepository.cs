using PayrollProject.DataModels;
using PayrollProject.ViewModel.EmployeeViewModels;

namespace PayrollProject.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(Guid employeeId);
        Task AddEmployeeAsync (Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Guid employeeId);
    }
}
