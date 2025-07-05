using CloudWorks.Application.DTOs.Schedules;

namespace CloudWorks.Application.DTOs.Bookings;

public record UpdateBookingDTO
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public List<Guid> SiteProfiles { get; set; } = [];
    public List<Guid> AccessPoints { get; set; } = [];
    public List<ScheduleRequestDTO> Schedules { get; set; } = [];
}