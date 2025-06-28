using CloudWorks.Application.DTOs.Schedules;

namespace CloudWorks.Application.DTOs.Bookings;

public class AddBookingRequest
{
    public string Name { get; set; }
    public List<Guid> SiteProfiles { get; set; } = [];
    public List<Guid> AccessPoints { get; set; } = [];
    public List<ScheduleRequest> Schedules { get; set; } = [];
}