using PayrollProject.DataModels;

namespace PayrollProject.Repositories.Interfaces
{
    public interface IDesignationRepository
    {
        Task<IEnumerable<Designation>> GetAllDesignationsAsync();
        Task<Designation?> GetDesignationByIdAsync(Guid designationId);
        Task AddDesignationAsync(Designation designation);
        Task UpdateDesignationAsync(Designation designation);
        Task DeleteDesignationAsync(Guid designationId);
    }
}

