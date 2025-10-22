using PayrollProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.ViewModel.EmployeeViewModels;

namespace PayrollProject.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync (Guid employeeId)
        {
            return await _context.Employees.FindAsync(employeeId);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await  _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(Guid employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
