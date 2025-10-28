using PayrollProject.DataModels;

namespace PayrollProject.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department?> GetDepartmentsByIdAsync(Guid departmentId);
        Task AddDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(Guid departmentId);
        Task<bool> DepartmentNameExistsAsync (string departmentName, Guid departmentId);
    }
}
