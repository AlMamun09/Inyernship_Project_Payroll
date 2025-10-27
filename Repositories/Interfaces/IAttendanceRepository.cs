using PayrollProject.DataModels;
using PayrollProject.ViewModel;
namespace PayrollProject.Repositories.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAllAttendancesAsync();
        Task<Attendance?> GetAttendanceByIdAsync(Guid attendanceId);
        Task AddAttendanceAsync(Attendance attendance);
        Task UpdateAttendanceAsync(Attendance attendance);
        Task DeleteAttendanceAsync(Guid attendanceId);
    }
}