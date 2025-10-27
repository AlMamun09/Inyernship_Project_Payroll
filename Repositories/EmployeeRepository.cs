using PayrollProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.ViewModel;

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

        public async Task<int> GetMaxEmployeeNumericIdAsync()
        {
            return await _context.Employees.AnyAsync()
                ? await _context.Employees.MaxAsync(e=> e.EmployeeNumericId) : 0;
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
