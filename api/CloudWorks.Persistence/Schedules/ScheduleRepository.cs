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

        private (DateTime Start, DateTime End) ParseScheduleValue(string icalValue)
        {
            if (string.IsNullOrWhiteSpace(icalValue))
                throw new ArgumentException("iCal data must not be null or empty.", nameof(icalValue));

            try
            {
                var calendar = Ical.Net.Calendar.Load(icalValue); // ✅ Correct for Ical.Net 5.x and newer

                var calendarEvent = calendar.Events.FirstOrDefault()
                    ?? throw new InvalidOperationException("No calendar event found");

                var start = calendarEvent.Start.Value.ToLocalTime();
                var end = calendarEvent.End.Value.ToLocalTime();

                return (start, end);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to parse iCal: {ex.Message}", ex);
            }
        }

        public async Task<List<(Guid AccessPointId, DateTime Start, DateTime End)>> GetOccupiedSlotsByAccessPointAsync(List<Guid> accessPointIds, DateTime start, DateTime end, CancellationToken cancellationToken)
        {
            var schedules = await _context.Schedules
                .Where(s => s.Booking.AccessPoints.Any(ap => accessPointIds.Contains(ap.Id)))
                .Select(s => new { s.Value, s.Booking.AccessPoints })
                .ToListAsync(cancellationToken);

            var result = new List<(Guid, DateTime, DateTime)>();

            foreach (var item in schedules)
            {
                var (parsedStart, parsedEnd) = ParseScheduleValue(item.Value);
                if (parsedEnd < start || parsedStart > end)
                    continue;

                foreach (var ap in item.AccessPoints.Where(ap => accessPointIds.Contains(ap.Id)))
                {
                    result.Add((ap.Id, parsedStart, parsedEnd));
                }
            }

            return result;
        }

        public async Task<List<(Guid AccessPointId, DateTime Start, DateTime End)>> GetUserAccessRangesAsync(Guid userId, List<Guid> accessPointIds, DateTime start, DateTime end, CancellationToken cancellationToken)
        {
            var raw = await _context.Schedules
                .Where(s => s.Booking.Profiles.Any(p => p.Id == userId) &&
                            s.Booking.AccessPoints.Any(ap => accessPointIds.Contains(ap.Id)))
                .Select(s => new { s.Value, s.Booking.AccessPoints })
                .ToListAsync(cancellationToken);

            var result = new List<(Guid, DateTime, DateTime)>();

            foreach (var item in raw)
            {
                var (parsedStart, parsedEnd) = ParseScheduleValue(item.Value);
                if (parsedEnd < start || parsedStart > end)
                    continue;

                foreach (var ap in item.AccessPoints.Where(ap => accessPointIds.Contains(ap.Id)))
                {
                    result.Add((ap.Id, parsedStart, parsedEnd));
                }
            }

            return result;
        }
    }
}