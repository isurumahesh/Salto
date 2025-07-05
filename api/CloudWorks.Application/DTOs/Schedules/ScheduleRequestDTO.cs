namespace CloudWorks.Application.DTOs.Schedules;

public record ScheduleRequestDTO
{
    public DateTime StartUtc { get; init; }
    public DateTime EndUtc { get; init; }
}