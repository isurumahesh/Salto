using CloudWorks.Data.Contracts.Models;

namespace CloudWorks.Services.Contracts.Schedules
{
    public interface IScheduleRepository
    {
        Task<List<OccupiedSlotDto>> GetOccupiedSlotsByAccessPointAsync(List<Guid> accessPointIds, DateTime start, DateTime end, CancellationToken cancellationToken);

        Task<List<OccupiedSlotDto>> GetUserAccessRangesAsync(Guid userId, List<Guid> accessPointIds, DateTime start, DateTime end, CancellationToken cancellationToken);
    }
}