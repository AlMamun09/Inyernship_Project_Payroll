using PayrollProject.DataModels;
using PayrollProject.ViewModel.LeaveViewModels;
namespace PayrollProject.Repositories.Interfaces
{
    public interface ILeaveRepository
    {
        Task<IEnumerable<Leave>> GetAllLeavesAsync();
        Task<Leave?> GetLeaveByIdAsync(Guid leaveId);
        Task AddLeaveAsync(Leave leave);
        Task UpdateLeaveAsync(Leave leave);
        Task DeleteLeaveAsync(Guid leaveId);
    }
}