using CloudWorks.Data.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Services.Contracts.Profiles
{
    public interface IProfileRepository
    {
        Task AddAsync(Profile profile, CancellationToken cancellationToken);
        Task<Profile?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<Profile?> GetByIdAsync(Guid id, CancellationToken cancellationToken); 
        Task<List<Profile>> GetAllAsync(CancellationToken cancellationToken);
    }
}
