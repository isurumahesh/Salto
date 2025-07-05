using CloudWorks.Data.Contracts.Models;
using CloudWorks.Data.Database;
using CloudWorks.Services.Contracts.Schedules;
using Ical.Net.Serialization;
using Microsoft.EntityFrameworkCore;

namespace CloudWorks.Persistence.Schedules
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly CloudWorksDbContext _context;
        private readonly CalendarSerializer _serializer = new();

        public ScheduleRepository(CloudWorksDbContext context)
        {
            _context = context;
        }

        public async Task<List<OccupiedSlotDto>> GetOccupiedSlotsByAccessPointAsync(List<Guid> accessPointIds, DateTime start, DateTime end, CancellationToken cancellationToken)
        {
            var schedules = await _context.Schedules
                .Where(s =>
                    s.StartUtc < end &&
                    s.EndUtc > start &&
                    s.Booking.AccessPoints.Any(ap => accessPointIds.Contains(ap.Id)))
                .Include(s => s.Booking.AccessPoints)
                .ToListAsync(cancellationToken);

            var result = new List<OccupiedSlotDto>();
            foreach (var schedule in schedules)
            {
                foreach (var ap in schedule.Booking.AccessPoints)
                {
                    if (accessPointIds.Contains(ap.Id))
                    {
                        result.Add(new OccupiedSlotDto
                        {
                            AccessPointId = ap.Id,
                            Start = schedule.StartUtc,
                            End = schedule.EndUtc
                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<OccupiedSlotDto>> GetUserAccessRangesAsync(
            Guid userId,
            List<Guid> accessPointIds,
            DateTime start,
            DateTime end,
            CancellationToken cancellationToken)
        {
            var schedules2 = await _context.Schedules
                .Where(s =>
                    s.StartUtc < end &&
                    s.EndUtc > start &&
                    s.Booking.Profiles.Any(p => p.ProfileId == userId)).ToListAsync(cancellationToken);

            var schedules = await _context.Schedules
                .Where(s =>
                    s.StartUtc < end &&
                    s.EndUtc > start &&
                    s.Booking.Profiles.Any(p => p.ProfileId == userId) &&
                    s.Booking.AccessPoints.Any(ap => accessPointIds.Contains(ap.Id)))
                .Include(s => s.Booking.AccessPoints)
                .ToListAsync(cancellationToken);

            var result = new List<OccupiedSlotDto>();
            foreach (var schedule in schedules)
            {
                foreach (var ap in schedule.Booking.AccessPoints)
                {
                    if (accessPointIds.Contains(ap.Id))
                    {
                        var actualStart = schedule.StartUtc > start ? schedule.StartUtc : start;
                        var actualEnd = schedule.EndUtc < end ? schedule.EndUtc : end;

                        if (actualEnd > actualStart)
                        {
                            result.Add(new OccupiedSlotDto
                            {
                                AccessPointId = ap.Id,
                                Start = actualStart,
                                End = actualEnd
                            });
                        }
                    }
                }
            }
            return result;
        }
    }
}