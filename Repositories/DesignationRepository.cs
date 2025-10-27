using Microsoft.EntityFrameworkCore;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.Repositories.Interfaces;

namespace PayrollProject.Repositories
{
    public class DesignationRepository : IDesignationRepository
    {
        private readonly ApplicationDbContext _context;
        public DesignationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Designation>> GetAllDesignationsAsync()
        {
            return await _context.Designations.ToListAsync();
        }

        public async Task<Designation?> GetDesignationByIdAsync(Guid designationId)
        {
            return await _context.Designations.FindAsync(designationId);
        }

        public async Task AddDesignationAsync(Designation designation)
        {
            await _context.Designations.AddAsync(designation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDesignationAsync(Designation designation)
        {
            _context.Designations.Update(designation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDesignationAsync(Guid designationId)
        {
            var designation = await _context.Designations.FindAsync(designationId);
            if (designation != null)
            {
                _context.Designations.Remove(designation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
