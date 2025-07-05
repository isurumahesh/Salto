using CloudWorks.Data.Contracts.Entities;

namespace CloudWorks.Services.Contracts.Profiles
{
    public interface IProfileRepository
    {
        Task<Profile> AddAsync(Profile profile, CancellationToken cancellationToken);

        Task<Profile?> GetByEmailAsync(string email, CancellationToken cancellationToken);

        Task<Profile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Profile>> GetAllAsync(CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}