namespace CloudWorks.Services.Contracts.Schedules
{
    public interface IScheduleRepository
    {
        Task<List<(Guid AccessPointId, DateTime Start, DateTime End)>> GetOccupiedSlotsByAccessPointAsync(List<Guid> accessPointIds, DateTime start, DateTime end, CancellationToken cancellationToken);

        Task<List<(Guid AccessPointId, DateTime Start, DateTime End)>> GetUserAccessRangesAsync(Guid userId, List<Guid> accessPointIds, DateTime start, DateTime end, CancellationToken cancellationToken);
    }
}