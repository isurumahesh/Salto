namespace CloudWorks.Application.DTOs.Schedules
{
    public class AccessPointTimeSlotDto
    {
        public Guid AccessPointId { get; set; }
        public List<TimeSlotDTO> TimeSlots { get; set; } = new();
    }
}