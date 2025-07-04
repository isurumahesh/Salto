﻿using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Database;
using CloudWorks.Services.Contracts.SiteProfiles;
using Microsoft.EntityFrameworkCore;

namespace CloudWorks.Persistence.SiteProfiles
{
    public class SiteProfileRepository : ISiteProfileRepository
    {
        private readonly CloudWorksDbContext _context;

        public SiteProfileRepository(CloudWorksDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SiteProfile siteProfile, CancellationToken cancellationToken)
        {
            await _context.SiteProfiles.AddAsync(siteProfile, cancellationToken);
        }

        public async Task<SiteProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
          await _context.SiteProfiles.FindAsync(id, cancellationToken);

        public async Task<List<SiteProfile>> GetBySiteIdAsync(Guid siteId, CancellationToken cancellationToken)
        {
            return await _context.SiteProfiles
                .Include(sp => sp.Profile)
                .Where(sp => sp.SiteId == siteId)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<SiteProfile>> GetByProfileIdAsync(Guid profileId, CancellationToken cancellationToken)
        {
            return await _context.SiteProfiles
                .Where(sp => sp.ProfileId == profileId).ToListAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}