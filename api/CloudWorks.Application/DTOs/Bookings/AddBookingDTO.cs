using CloudWorks.Application.DTOs.Schedules;

namespace CloudWorks.Application.DTOs.Bookings;

public record AddBookingDTO
{
    public string Name { get; init; }
    public List<Guid> SiteProfiles { get; init; } = [];
    public List<Guid> AccessPoints { get; init; } = [];
    public List<ScheduleRequestDTO> Schedules { get; init; } = [];
}