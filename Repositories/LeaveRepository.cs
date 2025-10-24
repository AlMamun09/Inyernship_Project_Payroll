using Microsoft.EntityFrameworkCore;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.Repositories.Interfaces;
using PayrollProject.ViewModel.LeaveViewModels;
namespace PayrollProject.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaveRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Leave>> GetAllLeavesAsync()
        {
            return await _context.Leaves.ToListAsync();
        }

        public async Task<Leave?> GetLeaveByIdAsync(Guid leaveId)
        {
            return await _context.Leaves.FindAsync(leaveId);
        }

        public async Task AddLeaveAsync(Leave leave)
        {
            await _context.Leaves.AddAsync(leave);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLeaveAsync(Leave leave)
        {
            _context.Leaves.Update(leave);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLeaveAsync(Guid leaveId)
        {
            var leave = await _context.Leaves.FindAsync(leaveId);
            if (leave != null)
            {
                _context.Leaves.Remove(leave);
                await _context.SaveChangesAsync();
            }
        }
    }
}