using CloudWorks.Application.DTOs.TimeSlots;

namespace CloudWorks.Application.DTOs.Schedules
{
    public record AccessPointTimeSlotDto
    {
        public Guid AccessPointId { get; init; }
        public List<TimeSlotDTO> TimeSlots { get; init; } = [];
    }
}