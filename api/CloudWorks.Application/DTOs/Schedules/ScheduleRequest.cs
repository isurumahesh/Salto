namespace CloudWorks.Application.DTOs.Schedules;

public class ScheduleRequest
{
    public DateTime StartUtc { get; set; }
    public DateTime EndUtc { get; set; }
}