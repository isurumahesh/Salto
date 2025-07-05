using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Database;
using CloudWorks.Services.Contracts.Profiles;
using Microsoft.EntityFrameworkCore;

namespace CloudWorks.Persistence.Profiles
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly CloudWorksDbContext _context;

        public ProfileRepository(CloudWorksDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Profile profile, CancellationToken cancellationToken)
        {
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Profile?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Profiles.FirstOrDefaultAsync(p => p.Email == email, cancellationToken);
        }

        public async Task<Profile?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Profiles.FindAsync(id, cancellationToken);
        }

        public async Task<List<Profile>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Profiles
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}