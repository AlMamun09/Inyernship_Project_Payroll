using PayrollProject.DataModels;
using PayrollProject.ViewModel;
namespace PayrollProject.Repositories.Interfaces
{
    public interface IShiftRepository
    {
        Task<IEnumerable<Shift>> GetAllShiftsAsync();
        Task<Shift?> GetShiftByIdAsync(Guid shiftId);
        Task AddShiftAsync(Shift shift);
        Task UpdateShiftAsync(Shift shift);
        Task DeleteShiftAsync(Guid shiftId);
    }
}