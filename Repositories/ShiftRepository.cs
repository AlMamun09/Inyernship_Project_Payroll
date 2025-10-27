using Microsoft.EntityFrameworkCore;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.Repositories.Interfaces;
using PayrollProject.ViewModel;
namespace PayrollProject.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly ApplicationDbContext _context;

        public ShiftRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shift>> GetAllShiftsAsync()
        {
            return await _context.Shifts.ToListAsync();
        }

        public async Task<Shift?> GetShiftByIdAsync(Guid shiftId)
        {
            return await _context.Shifts.FindAsync(shiftId);
        }

        public async Task AddShiftAsync(Shift shift)
        {
            await _context.Shifts.AddAsync(shift);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateShiftAsync(Shift shift)
        {
            _context.Shifts.Update(shift);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShiftAsync(Guid shiftId)
        {
            var shift = await _context.Shifts.FindAsync(shiftId);
            if (shift != null)
            {
                _context.Shifts.Remove(shift);
                await _context.SaveChangesAsync();
            }
        }
    }
}